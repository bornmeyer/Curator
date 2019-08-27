using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class Signature
    {
        // Fields

        private readonly String _signatureName = String.Empty;
        private readonly Byte[] _signatureData = null;

        // Properties

        public String SignatureName => _signatureName;

        public Byte[] SignatureData => _signatureData;

        // Constructors

        public Signature(String signatureName, Byte[] signatureData)
        {
            _signatureName = signatureName;
            _signatureData = signatureData;
        }
    }
}
