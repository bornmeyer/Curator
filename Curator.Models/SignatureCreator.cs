using FastRsync.Signature;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class SignatureCreator : ISignatureCreator
    {
        // Methods

        public Byte[] CreateSignature(FileNode node)
        {
            Byte[] result = null;

            var signatureBuilder = new SignatureBuilder();
            using (var basisStream = new FileStream(Path.Combine(node.Directory, node.FileName), FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var signatureStream = new MemoryStream())
            {
                signatureBuilder.Build(basisStream, new SignatureWriter(signatureStream));
                signatureStream.Position = 0;
                result = signatureStream.ToArray();
            }

            return result;
        }
    }
}
