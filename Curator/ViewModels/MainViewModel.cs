using Curator.Models;
using System;
using System.Collections.Generic;
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

        // Constructors

        public MainViewModel(IFileManager fileManager)
        {
            _fileManager = fileManager;
            var configs = _fileManager.GetAllConfigurations();
            var file = new FileInfo("test.txt");
            _fileManager.Manage(file);
        }
    }
}
