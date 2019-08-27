using System;

namespace Curator.Models
{
    public interface IFileRestore
    {
        (Byte[] Result, DeltaFileTransaction Transaction) Restore(FileNode node, LogEntry entry);
    }
}