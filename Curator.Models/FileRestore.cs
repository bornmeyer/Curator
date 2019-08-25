using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileRestore : IFileRestore
    {
        // Fields

        private readonly IArchiveManager _archiveManager = null;
        private readonly IDeltaPatcher _deltaPatcher = null;

        // Constructors

        public FileRestore(IArchiveManager archiveManager, IDeltaPatcher deltaPatcher)
        {
            _archiveManager = archiveManager;
            _deltaPatcher = deltaPatcher;
        }

        // Methods

        public Byte[] Restore(FileNode node, LogEntry entry)
        {
            Byte[] result = null;
            var logEntries = node.LogEntries.Where(x => x.CreatedAt <= entry.CreatedAt);

            foreach (var current in logEntries)
            {
                switch (current.Type)
                {
                    case LogEntryTypes.Genesis:
                        result = _archiveManager.Read(node.FileName, node);
                        break;
                    case LogEntryTypes.Normal:
                        var delta = _archiveManager.Read(current.DiffName, node);
                        result = _deltaPatcher.Patch(result, delta);
                        break;
                    default:
                        break;
                }
            }

            Trace.Write(System.Text.Encoding.Default.GetString(result));
            return result;
        }
    }
}
