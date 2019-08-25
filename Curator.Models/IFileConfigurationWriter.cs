using System.Collections.Generic;

namespace Curator.Models
{
    public interface IFileConfigurationWriter
    {
        void Write(IEnumerable<FileNode> fileConfigurations);
    }
}