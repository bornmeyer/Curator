using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileHandlingStrategySelector : IFileHandlingStrategySelector
    {
        // Fields

        private readonly IEnumerable<IFileHandlingStrategy> _fileHandlingStrategies = null;

        public FileHandlingStrategySelector(IEnumerable<IFileHandlingStrategy> fileHandlingStrategies)
        {
            _fileHandlingStrategies = fileHandlingStrategies;
        }

        // Methods

        public IFileHandlingStrategy Select(FileNode node)
        {
            return _fileHandlingStrategies.FirstOrDefault(x => x.CanHandle(node));
        }
    }
}
