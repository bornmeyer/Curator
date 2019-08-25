using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public interface IDeltaCreationStrategy
    {
        Boolean CanHandle(FileNode node);

        FileNode CreateDelta(FileNode node);
    }
}
