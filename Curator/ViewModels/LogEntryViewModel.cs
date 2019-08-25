using Curator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Curator.ViewModels
{
    public class LogEntryViewModel : ViewModelBase
    {
        // Event

        public event Action<LogEntry> RestoreRequested;

        // Fields

        private DateTime _createdAt = default(DateTime);
        private readonly LogEntry _logEntry = null;

        // Properties
    

        public DateTime CreatedAt
        {
            get
            {
                return _createdAt;
            }
            set
            {
                _createdAt = value;
                OnPropertyChanged();
            }
        }

        public ICommand RestoreCommand
        {
            get;
            private set;
        }

        // Constructors

        public LogEntryViewModel(LogEntry logEntry, Action<LogEntry> callback)
        {
            RestoreCommand = new RelayCommand<Object>(Restore);
            CreatedAt = logEntry.CreatedAt;
            _logEntry = logEntry;
            RestoreRequested += callback;
        }

        private void Restore(Object obj)
        {
            OnRestoreRequested(_logEntry);
        }

        private void OnRestoreRequested(LogEntry entry)
        {
            RestoreRequested?.Invoke(entry);
        }
    }
}
