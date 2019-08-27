using Curator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Curator.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        // Fields

        private readonly IFileManager _fileManager = null;
        private FileNodeControlViewModel _selectedFileNodeControlViewModel = null;
        private readonly IDispatcherService _dispatcherService = null;
        private readonly IFileSelectService _fileSelectService = null;

        // Properties

        public ObservableCollection<FileNodeControlViewModel> FileNodeControlViewModels
        {
            get;
            private set;
        }
        public FileNodeControlViewModel SelectedFileNodeControlViewModel
        {
            get
            {
                return _selectedFileNodeControlViewModel;
            }
            set
            {
                _selectedFileNodeControlViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectFileCommand
        {
            get;
            private set;
        }

        // Constructors

        public MainViewModel(IFileManager fileManager,
            IFileSelectService fileSelectService,
            IDispatcherService dispatcherService)
        {
            _dispatcherService = dispatcherService;
            _fileSelectService = fileSelectService;
            _fileSelectService.FileSelected += OnFileSelected;
            FileNodeControlViewModels = new ObservableCollection<FileNodeControlViewModel>();
            SelectFileCommand = new RelayCommand<Object>(OnSelectFileCommand);
            _fileManager = fileManager;
            _fileManager.NodeUpdated += OnNodeUpdated;
            var configs = _fileManager.GetAllConfigurations();

            foreach (var current in configs)
            {
                FileNodeControlViewModels.Add(new FileNodeControlViewModel(current, RestoreRequested));
            }
            SelectedFileNodeControlViewModel = FileNodeControlViewModels.FirstOrDefault();
        }

        // Methods

        private void OnNodeUpdated(FileNode node)
        {
            _dispatcherService.Invoke(() =>
            {
                var matchingViewModel = FileNodeControlViewModels.FirstOrDefault(x => x.Node == node);
                matchingViewModel?.UpdateNode(node);
            });
        }

        private void OnFileSelected(FileInfo file)
        {
            var node = _fileManager.Manage(file);
            if (node != null)
            {
                FileNodeControlViewModels.Add(new FileNodeControlViewModel(node, RestoreRequested));
            }
        }

        private void RestoreRequested(FileNode node, LogEntry logEntry)
        {
            _fileManager.Restore(node, logEntry);
        }

        private void OnSelectFileCommand(Object obj)
        {
            _fileSelectService.OpenDialogue();
        }
    }
}
