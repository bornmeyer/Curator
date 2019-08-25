using Curator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.ViewModels
{
    public class FileNodeControlViewModel : ViewModelBase
    {
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

        public FileNodeControlViewModel(FileNode node)
        {
            IsCollapsed = true;
            Node = node;
            NumberOfVersions = Node.LogEntries.Count();
        }
    }
}
