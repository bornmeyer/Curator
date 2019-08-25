using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class LogEntry
    {
        public String DiffName
        {
            get;
            set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public LogEntryTypes Type
        {
            get;
            set;
        }

        public String SignatureEntry
        {
            get;
            set;
        }
    }
}
