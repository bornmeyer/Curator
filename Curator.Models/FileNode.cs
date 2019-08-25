using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileNode
    {
        public String FileName
        {
            get;
            set;
        }
        public String Directory
        {
            get;
            set;
        }

        public List<LogEntry> LogEntries
        {
            get;
            set;
        }

        public String ArchivePath
        {
            get;
            set;
        }

        // Constructors

        public FileNode(FileInfo fileInfo)
        {
            FileName = fileInfo.Name;
            Directory = fileInfo.Directory.FullName;
            LogEntries = new List<LogEntry>();
            ArchivePath = $"{fileInfo.FullName}.zip";
        }

        public FileNode()
        {

        }

        // Methods

        public override string ToString()
        {
            return $"#{LogEntries.Count()} LogEntries for #{FileName}";
        }

        public override bool Equals(object obj)
        {
            Boolean result = false;

            if (obj is FileNode other)
                result = String.CompareOrdinal(other.FileName, FileName) == 0;
            return result;
        }
    }
}
