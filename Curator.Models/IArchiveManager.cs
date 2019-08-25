using System;
using System.IO;

namespace Curator.Models
{
    public interface IArchiveManager
    {
        void AppendFile(FileNode node);
        void Append(String entryName, FileNode node, Byte[] contents);

        Byte[] Read(String entryName, FileNode node);
    }
}