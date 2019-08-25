using System;
using System.IO;

namespace Curator.Models
{
    public interface IFileWatcher
    {
        event Action<FileInfo> FileChanged;
    }
}