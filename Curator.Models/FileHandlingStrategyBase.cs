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

        public event Action<IFileHandlingStrategy, DeltaFileTransaction> Handled;

        // Methods

        public abstract Boolean CanHandle(FileNode node);

        public abstract void Handle(FileNode node);

        public abstract Task HandleAsync(FileNode node);

        protected virtual void OnHandled(DeltaFileTransaction transaction)
        {
            Handled?.Invoke(this, transaction);
        }
    }
}
