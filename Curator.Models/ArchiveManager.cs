using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class ArchiveManager : IArchiveManager
    {
        public void Append(String entryName, FileNode node, Byte[] contents)
        {
            try
            {
                using (ZipArchive zipArchive = ZipFile.Open(node.ArchivePath, ZipArchiveMode.Update))
                {
                    var newEntry = zipArchive.CreateEntry(entryName);
                    using (BinaryWriter streamWriter = new BinaryWriter(newEntry.Open()))
                    {
                        streamWriter.Write(contents);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        public void AppendFile(FileNode node)
        {
            try
            {
                using (var zipArchive = ZipFile.Open(node.ArchivePath, ZipArchiveMode.Create))
                {
                    FileInfo fileInfo = new FileInfo(Path.Combine(node.Directory, node.FileName));
                    zipArchive.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name, CompressionLevel.Optimal);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        public Byte[] Read(String entryName, FileNode node)
        {
            Byte[] result = null;

            try
            {
                using (var zipArchive = ZipFile.Open(node.ArchivePath, ZipArchiveMode.Read))
                {
                    var entry = zipArchive.GetEntry(entryName);
                    
                    using (var stream = entry.Open())
                    {
                        var memoryStream = new MemoryStream();
                        stream.CopyTo(memoryStream);
                        result = memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return result;
        }
    }
}
