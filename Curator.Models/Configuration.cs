using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class Configuration
    {
        public List<FileNode> Nodes
        {
            get;
            set;
        }

        public Configuration()
        {
            Nodes = new List<FileNode>();
        }
    }
}
