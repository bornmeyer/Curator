using FastRsync.Core;
using FastRsync.Delta;
using FastRsync.Diagnostics;
using FastRsync.Signature;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class DeltaCreator : IDeltaCreator
    {
        public Byte[] BuildDelta(Byte[] signature, FileNode node)
        {
            Byte[] result = null;
            var deltaBuilder = new DeltaBuilder();
            deltaBuilder.ProgressReport = new ConsoleProgressReporter();

            using (var newFileStream = new FileStream(Path.Combine(node.Directory, node.FileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var signatureStream = new MemoryStream(signature))
            using (var deltaStream = new MemoryStream())
            {
                deltaBuilder.BuildDelta(newFileStream, new SignatureReader(signatureStream, deltaBuilder.ProgressReport), new AggregateCopyOperationsDecorator(new BinaryDeltaWriter(deltaStream)));
                deltaStream.Position = 0;
                result = deltaStream.ToArray();
            }
            return result;
        }
    }
}
