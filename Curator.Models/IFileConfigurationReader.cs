using System.Collections.Generic;

namespace Curator.Models
{
    public interface IFileConfigurationReader
    {
        IList<FileNode> Read();
    }
}