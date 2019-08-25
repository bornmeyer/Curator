using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class AppendLogFileHandlingStrategy : FileHandlingStrategyBase
    {
        // Fields

        private readonly IArchiveManager _archiveManager = null;
        private readonly IEnumerable<IDeltaCreationStrategy> _deltaCreationStrategies = null;


        // Constructors

        public AppendLogFileHandlingStrategy(IArchiveManager archiveManager, IEnumerable<IDeltaCreationStrategy> deltaCreationStrategies)
        {
            _archiveManager = archiveManager;
            _deltaCreationStrategies = deltaCreationStrategies;
        }

        // Methods

        public override bool CanHandle(FileNode node)
        {
            return node.LogEntries.Count() > 0;
        }

        public override void Handle(FileNode node)
        {
            var strategy = _deltaCreationStrategies.FirstOrDefault(x => x.CanHandle(node));
            var updatedNode = strategy.CreateDelta(node);
            OnHandled(node);
        }
    }
}
