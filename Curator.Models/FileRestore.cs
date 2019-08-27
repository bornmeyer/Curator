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

        private readonly IDeltaCreator _deltaCreator = null;
        private readonly ISignatureCreator _signatureCreator = null;
        private readonly IFileHandlingStrategySelector _fileHandlingStrategySelector = null;
        private readonly IArchiveManager _archiveManager = null;
        private readonly IDeltaPatcher _deltaPatcher = null;

        // Constructors

        public FileRestore(IArchiveManager archiveManager, 
            IDeltaPatcher deltaPatcher, 
            ISignatureCreator signatureCreator,
            IDeltaCreator deltaCreator,
            IFileHandlingStrategySelector fileHandlingStrategySelectors)
        {
            _deltaCreator = deltaCreator;
            _signatureCreator = signatureCreator;
            _fileHandlingStrategySelector = fileHandlingStrategySelectors;
            _archiveManager = archiveManager;
            _deltaPatcher = deltaPatcher;
        }

        // Methods

        public (Byte[] Result, DeltaFileTransaction Transaction) Restore(FileNode node, LogEntry entry)
        {
            Byte[] result = null;
            Byte[] lastRead = null;
            var logEntries = node.LogEntries.Where(x => x.CreatedAt <= entry.CreatedAt);
            var strategy = _fileHandlingStrategySelector.Select(node);
            foreach (var current in logEntries)
            {
                switch (current.Type)
                {
                    case LogEntryTypes.Genesis:
                        result = _archiveManager.Read(node.FileName, node);
                        break;
                    case LogEntryTypes.Normal:
                        var delta = _archiveManager.Read(current.DiffName, node);
                        lastRead = result;
                        result = _deltaPatcher.Patch(result, delta);
                        break;
                    default:
                        break;
                }
            }

            var newSignature = _signatureCreator.CreateSignature(result);
            var newDelta = _deltaCreator.BuildDelta(newSignature, lastRead);
            var transaction = new DeltaFileTransaction(node,
                new Signature($"{Guid.NewGuid().ToString()}.signature", newSignature),
                new Delta($"{Guid.NewGuid().ToString()}.delta", newDelta));
            
            return (result, transaction);
        }
    }
}
