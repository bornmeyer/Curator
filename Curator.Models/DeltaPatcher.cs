using FastRsync.Delta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class DeltaPatcher : IDeltaPatcher
    {
        public Byte[] Patch(Byte[] content, Byte[] delta)
        {
            Byte[] result = null;

            var deltaApplier = new DeltaApplier();


            using (var contentStream = new MemoryStream(content))
            using (var deltaStream = new MemoryStream(delta))
            using (var resultStream = new MemoryStream())
            {
                deltaApplier.Apply(contentStream, new BinaryDeltaReader(deltaStream, null), resultStream);
                resultStream.Position = 0;
                result = resultStream.ToArray();
            }

            return result;
        }
    }
}
