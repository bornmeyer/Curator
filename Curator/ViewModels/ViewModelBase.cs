using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Curator.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // Events

        public event PropertyChangedEventHandler PropertyChanged;

        // Methods

        protected void OnPropertyChanged([CallerMemberName]String caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
