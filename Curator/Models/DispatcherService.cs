using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Curator.Models
{
    public class DispatcherService : IDispatcherService
    {

        public void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public void InvokeAsync(Action action)
        {
            Application.Current.Dispatcher.InvokeAsync(action);
        }
    }
}
