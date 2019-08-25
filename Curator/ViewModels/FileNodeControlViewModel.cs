using Curator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.ViewModels
{
    public class FileNodeControlViewModel : ViewModelBase
    {
        // Events

        public event Action<FileNode, LogEntry> RestoreRequested;

        // Fields

        private FileNode _node;
        private Int32 _numberOfVersions = 0;
        private bool _isCollapsed;

        // Properties

        public FileNode Node
        {
            get
            {
                return _node;
            }
            set
            {
                _node = value;                
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LogEntryViewModel> LogEntryViewModels
        {
            get;
            private set;
        }

        public Int32 NumberOfVersions
        {
            get
            {
                return _numberOfVersions;
            }
            set
            {
                _numberOfVersions = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsCollapsed
        {
            get
            {
                return _isCollapsed;
            }
            set
            {
                _isCollapsed = value;
                OnPropertyChanged();
            }
        }

        // Constructors

        public FileNodeControlViewModel(FileNode node, Action<FileNode, LogEntry> callback)
        {
            IsCollapsed = true;
            Node = node;
            NumberOfVersions = Node.LogEntries.Count();
            var logEntryViewModels = node.LogEntries.Select(x => new LogEntryViewModel(x, y => OnRestoreRequested(_node, y)));           
            LogEntryViewModels = new ObservableCollection<LogEntryViewModel>(logEntryViewModels);
            RestoreRequested += callback;
        }

        // Methods

        private void OnRestoreRequested(FileNode node, LogEntry entry)
        {
            RestoreRequested?.Invoke(node, entry);
        }
    }
}
