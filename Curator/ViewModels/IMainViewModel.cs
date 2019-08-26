using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Curator.ViewModels
{
    public interface IMainViewModel
    {
        ObservableCollection<FileNodeControlViewModel> FileNodeControlViewModels { get; }

        FileNodeControlViewModel SelectedFileNodeControlViewModel
        {
            get;
            set;
        }

        ICommand SelectFileCommand
        {
            get;
        }
    }
}
