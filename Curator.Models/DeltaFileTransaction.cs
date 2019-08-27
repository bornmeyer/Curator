using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class DeltaFileTransaction
    {
        // Fields

        private readonly FileNode _node = null;
        private readonly Signature _signature = null;
        private readonly Delta _delta = null;

        // Properties

        public FileNode Node => _node;

        public Signature Signature => _signature;

        public Delta Delta => _delta;

        // Constructors

        public DeltaFileTransaction(FileNode node, Signature signature, Delta delta)
        {
            _node = node;
            _signature = signature;
            _delta = delta;
        }
    }
}
