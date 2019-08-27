using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class Delta
    {
        // Fields

        private readonly String _deltaName = String.Empty;
        private readonly Byte[] _deltaData = null;

        // Properties

        public String DeltaName => _deltaName;

        public Byte[] DeltaData => _deltaData;

        // Constructors

        public Delta(String deltaName, Byte[] deltaData)
        {
            _deltaName = deltaName;
            _deltaData = deltaData;
        }
    }
}
