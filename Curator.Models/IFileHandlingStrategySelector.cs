using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public interface IFileHandlingStrategySelector
    {
        IFileHandlingStrategy Select(FileNode node);
    }
}
