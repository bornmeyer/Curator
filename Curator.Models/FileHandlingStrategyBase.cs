using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public abstract class FileHandlingStrategyBase : IFileHandlingStrategy
    {
        // Events

        public event Action<IFileHandlingStrategy, FileNode> Handled;

        // Methods

        public abstract Boolean CanHandle(FileNode node);

        public abstract void Handle(FileNode node);

        protected virtual void OnHandled(FileNode node)
        {
            Handled?.Invoke(this, node);
        }
    }
}
