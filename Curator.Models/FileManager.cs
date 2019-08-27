using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileManager : IFileManager
    {
        // Events

        public event Action<FileNode> NodeUpdated;

        // Fields

        private readonly IFileConfigurationReader _fileConfigurationReader = null;
        private readonly IFileConfigurationWriter _fileConfigurationWriter = null;
        private readonly IFileWatcherFactory _fileWatcherFactory = null;
        private readonly IList<FileNode> _configuration = null;
        private readonly ITransactionWriter _transactionWriter = null;
        private readonly IFileRestore _fileRestore = null;
        private readonly IFileHandlingStrategySelector _fileHandlingStrategySelector = null;
        private readonly List<IFileWatcher> _fileWatchers = null;

        // Constructors

        public FileManager(IFileConfigurationReader fileConfigurationReader,
            IFileConfigurationWriter fileConfigurationWriter,
            IFileWatcherFactory fileWatcherFactory,
            IFileHandlingStrategySelector fileHandlingStrategySelector,
            IFileRestore fileRestore,
            ITransactionWriter transactionWriter)
        {
            _transactionWriter = transactionWriter;
            _fileRestore = fileRestore;
            _fileHandlingStrategySelector = fileHandlingStrategySelector;
            _fileWatchers = new List<IFileWatcher>();
            _fileConfigurationReader = fileConfigurationReader;
            _fileConfigurationWriter = fileConfigurationWriter;
            _fileWatcherFactory = fileWatcherFactory;
            _configuration = _fileConfigurationReader.Read();

            foreach (var current in _configuration)
            {
                CreateFileWatcher(current);
            }
        }

        // Methods

        private void CreateFileWatcher(FileNode current)
        {
            var path = Path.Combine(current.Directory, current.FileName);
            var watcher = _fileWatcherFactory.Create(new FileInfo(path));
            watcher.FileChanged += async (fileInfo) => await Watcher_FileChanged(fileInfo);
            _fileWatchers.Add(watcher);
            if(current.LogEntries.Count() == 0)
                watcher.Initialize();
        }

        private async Task Watcher_FileChanged(FileInfo fileInfo)
        {
            var node = _configuration.FirstOrDefault(x => Path.Combine(x.Directory, x.FileName) == fileInfo.FullName);
            var strategy = _fileHandlingStrategySelector.Select(node);
            strategy.Handled += Strategy_Handled;
            await strategy.HandleAsync(node);
            OnNodeUpdated(node);
        }

        private void Strategy_Handled(IFileHandlingStrategy strategy, DeltaFileTransaction transaction)
        {
            _transactionWriter.Write(transaction);
            _fileConfigurationWriter.Write(_configuration);
            strategy.Handled -= Strategy_Handled;
        }

        public IEnumerable<FileNode> GetAllConfigurations()
        {
            return _configuration;
        }

        public FileNode Manage(FileInfo fileInfo)
        {            
            var node = _configuration.FirstOrDefault(x => Path.Combine(x.Directory, x.FileName) == fileInfo.FullName);
            if (node == null)
            {
                var newNode = new FileNode(fileInfo);
                _configuration.Add(newNode);
                _fileConfigurationWriter.Write(_configuration);
                CreateFileWatcher(newNode);
                node = newNode;
            }
            return node;
        }

        private void OnNodeUpdated(FileNode node)
        {
            
            NodeUpdated?.Invoke(node);
        }

        public void Restore(FileNode node, LogEntry entry)
        {
            var path = Path.Combine(node.Directory, node.FileName);
            var fileWatcher = _fileWatchers.FirstOrDefault(x => x.ObservedFile.FullName == path);
            if (fileWatcher != null)
            {
                var restoredContents = _fileRestore.Restore(node, entry);
                fileWatcher.Pause();
                File.WriteAllBytes(path, restoredContents.Result);
                _transactionWriter.Write(restoredContents.Transaction);
                fileWatcher.Resume();
            }
        }
    }
}
