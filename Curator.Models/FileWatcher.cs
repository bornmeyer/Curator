using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileWatcher : IFileWatcher
    {
        // Events

        public event Action<FileInfo> FileChanged;

        // Fields

        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly FileInfo _fileInfo = null;
                
        // Properties

        public FileInfo ObservedFile => _fileInfo;

        // Constructors 

        public FileWatcher(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
            _fileSystemWatcher = new FileSystemWatcher(fileInfo.Directory.FullName, fileInfo.Name);
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
            _fileSystemWatcher.Changed += _fileSystemWatcher_Changed;
        }

        // Methods

        private void _fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                _fileSystemWatcher.EnableRaisingEvents = false;

                OnFileChanged(new FileInfo(e.FullPath));
            }
            finally
            {
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        private void OnFileChanged(FileInfo fileInfo)
        {
            FileChanged?.Invoke(fileInfo);
        }

        public void Pause()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
        }

        public void Resume()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
        }
    }
}
