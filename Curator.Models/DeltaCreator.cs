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
            var result = BuildDeltaFromStream(signature,
                () => new FileStream(Path.Combine(node.Directory, node.FileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            return result;
        }

        public Byte[] BuildDelta(Byte[] signature, Byte[] data)
        {           
            var result = BuildDeltaFromStream(signature, () => new MemoryStream(data));
            return result;
        }

        private Byte[] BuildDeltaFromStream<TStream>(Byte[] signature, Func<TStream> action)
            where TStream : Stream
        {
            Byte[] result = null;

            var deltaBuilder = new DeltaBuilder();
            deltaBuilder.ProgressReport = new ConsoleProgressReporter();

            using (var newFileStream = action())
            using (var signatureStream = new MemoryStream(signature))
            using (var deltaStream = new MemoryStream())
            {
                deltaBuilder.BuildDelta(newFileStream, 
                    new SignatureReader(signatureStream, deltaBuilder.ProgressReport), 
                    new AggregateCopyOperationsDecorator(new BinaryDeltaWriter(deltaStream)));
                deltaStream.Position = 0;
                result = deltaStream.ToArray();
            }
            return result;
        }
    }
}
