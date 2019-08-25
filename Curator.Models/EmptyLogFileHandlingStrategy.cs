
using FastRsync.Signature;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class EmptyLogFileHandlingStrategy : FileHandlingStrategyBase
    {
     
        // Fields

        private readonly IArchiveManager _archiveManager = null;
        private readonly ISignatureCreator _signatureCreator = null;

        // Constructors

        public EmptyLogFileHandlingStrategy(IArchiveManager archiveManager, ISignatureCreator signatureCreator)
        {
            _archiveManager = archiveManager;
            _signatureCreator = signatureCreator;
        }

        // Methods

        public override Boolean CanHandle(FileNode node)
        {
            return node.LogEntries.Count() == 0;
        }

        public override void Handle(FileNode node)
        {
            _archiveManager.AppendFile(node);
            var signatureName = $"{Guid.NewGuid().ToString()}.signature";
            var logEntry = new LogEntry
            {
                CreatedAt = DateTime.Now,
                DiffName = String.Empty,
                Type = LogEntryTypes.Genesis,
                SignatureEntry = signatureName
            };
            var signature = _signatureCreator.CreateSignature(node);
            _archiveManager.Append(signatureName, node, signature);
            node.LogEntries.Add(logEntry);



            OnHandled(node);
        }

    }
}
