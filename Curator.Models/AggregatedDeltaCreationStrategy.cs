using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class AggregatedDeltaCreationStrategy : IDeltaCreationStrategy
    {
        // Fields

        private readonly IDeltaCreator _deltaCreator = null;
        private readonly IDeltaPatcher _deltaPatcher = null;
        private readonly IArchiveManager _archiveManager = null;
        private readonly ISignatureCreator _signatureCreator = null;

        // Constructors

        public AggregatedDeltaCreationStrategy(IDeltaCreator deltaCreator, IDeltaPatcher deltaPatcher, IArchiveManager archiveManager, ISignatureCreator signatureCreator)
        {
            _deltaCreator = deltaCreator;
            _deltaPatcher = deltaPatcher;
            _archiveManager = archiveManager;
            _signatureCreator = signatureCreator;
        }

        // Methods

        public bool CanHandle(FileNode node)
        {
            return node.LogEntries.Count() > 1;
        }

        public FileNode CreateDelta(FileNode node)
        {
            var logEntry = node.LogEntries.First(x => x.Type == LogEntryTypes.Genesis);
            var original = _archiveManager.Read(node.FileName, node);

            var firstDelta = node.LogEntries.First(x => x.Type == LogEntryTypes.Normal);
            var delta = _archiveManager.Read(firstDelta.DiffName, node);
            var patched = _deltaPatcher.Patch(original, delta);

            foreach (var current in node.LogEntries.Where(x => x.Type == LogEntryTypes.Normal).Skip(1))
            {
                var rollingDelta = _archiveManager.Read(current.DiffName, node);
                patched = _deltaPatcher.Patch(patched, rollingDelta);
            }

            var deltaName = $"{Guid.NewGuid().ToString()}.delta";
            var mostRecentLogEntry = node.LogEntries.OrderBy(x => x.CreatedAt).Last();
            var signature = _archiveManager.Read(logEntry.SignatureEntry, node);
            var newDelta = _deltaCreator.BuildDelta(signature, node);
            _archiveManager.Append(deltaName, node, newDelta);

            String newSignatureName = $"{Guid.NewGuid().ToString()}.signature";
            Byte[] newSignature = _signatureCreator.CreateSignature(node);
            _archiveManager.Append(newSignatureName, node, newSignature);


            var newLogEntry = new LogEntry
            {
                CreatedAt = DateTime.Now,
                Type = LogEntryTypes.Normal,
                DiffName = deltaName,
                SignatureEntry = newSignatureName
            };
            node.LogEntries.Add(newLogEntry);
            return node;
        }
    }
}
