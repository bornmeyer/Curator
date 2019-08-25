using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileConfigurationReader : IFileConfigurationReader
    {
        // Fields

        private readonly IConfigFilePathLocator _configFilePathLocator = null;

        // Constructors

        public FileConfigurationReader(IConfigFilePathLocator configFilePathLocator)
        {
            _configFilePathLocator = configFilePathLocator;
        }

        // Methods

        public IList<FileNode> Read()
        {
            List<FileNode> result = null;

            var filePath = _configFilePathLocator.Locate();

            using (StreamReader fileReader = File.OpenText(filePath.FullName))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (List<FileNode>)serializer.Deserialize(fileReader, typeof(List<FileNode>));
            }

            return result ?? new List<FileNode>();
        }
    }
}
