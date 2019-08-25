using System;
using System.Collections.Generic;
using System.IO;

namespace Curator.Models
{
    public interface IFileManager
    {
        event Action<FileNode> NodeUpdated;

        IEnumerable<FileNode> GetAllConfigurations();
        void Manage(FileInfo fileInfo);

        void Restore(FileNode node, LogEntry entry);
    }
}