using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileWatcherFactory : IFileWatcherFactory
    {
        // Fields

        private readonly IKernel _kernel = null;

        // Constructors

        public FileWatcherFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        // Methods

        public IFileWatcher Create(FileInfo fileInfo)
        {
            return _kernel.Get<IFileWatcher>(new ConstructorArgument("fileInfo", fileInfo));
        }
    }
}
