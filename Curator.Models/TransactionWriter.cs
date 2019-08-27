using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class TransactionWriter : ITransactionWriter
    {
        // Fields

        private readonly IArchiveManager _archiveManager = null;

        // Constructors

        public TransactionWriter(IArchiveManager archiveManager)
        {
            _archiveManager = archiveManager;
        }

        // Methods

        public void Write(DeltaFileTransaction transaction)
        {
            _archiveManager.Append(transaction.Signature.SignatureName, transaction.Node, transaction.Signature.SignatureData);

            if(transaction.Delta != null)
            {
                _archiveManager.Append(transaction.Delta.DeltaName, transaction.Node, transaction.Delta.DeltaData);
            }
        }
    }
}
