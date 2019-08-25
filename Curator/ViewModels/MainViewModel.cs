﻿using Curator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        // Fields

        private readonly IFileManager _fileManager = null;

        // Properties

        public ObservableCollection<FileNodeControlViewModel> FileNodeControlViewModels
        {
            get;
            private set;
        }

        // Constructors

        public MainViewModel(IFileManager fileManager)
        {
            FileNodeControlViewModels = new ObservableCollection<FileNodeControlViewModel>();

            _fileManager = fileManager;
            var configs = _fileManager.GetAllConfigurations();
            var file = new FileInfo("test.txt");
            _fileManager.Manage(file);

            foreach (var current in configs)
            {
                FileNodeControlViewModels.Add(new FileNodeControlViewModel(current, RestoreRequested));
            }
        }

        // Methods

        private void RestoreRequested(FileNode node, LogEntry logEntry)
        {
            _fileManager.Restore(node, logEntry);
        }
    }
}
