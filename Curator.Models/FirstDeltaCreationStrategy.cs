using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FirstDeltaCreationStrategy : IDeltaCreationStrategy
    {
        // Fields

        private readonly IDeltaCreator _deltaCreator = null;
        private readonly IArchiveManager _archiveManager = null;
        private readonly ISignatureCreator _signatureCreator = null;

        // Constructors

        public FirstDeltaCreationStrategy(IDeltaCreator deltaCreator, IArchiveManager archiveManager, ISignatureCreator signatureCreator)
        {
            _deltaCreator = deltaCreator;
            _archiveManager = archiveManager;
            _signatureCreator = signatureCreator;
        }

        // Methods

        public Boolean CanHandle(FileNode node)
        {
            return node.LogEntries.Count() == 1;
        }

        public FileNode CreateDelta(FileNode node)
        {
            var orderedLogEntries = node.LogEntries.OrderBy(x => x.CreatedAt);

            var deltaName = $"{Guid.NewGuid().ToString()}.delta";
            var logEntry = orderedLogEntries.First();
            var signature = _archiveManager.Read(logEntry.SignatureEntry, node);
            var delta = _deltaCreator.BuildDelta(signature, node);
            _archiveManager.Append(deltaName, node, delta);

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
