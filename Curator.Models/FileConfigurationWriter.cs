using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileConfigurationWriter : IFileConfigurationWriter
    {
        // Fields

        private readonly IConfigFilePathLocator _configFilePathLocator = null;

        // Constructors

        public FileConfigurationWriter(IConfigFilePathLocator configFilePathLocator)
        {
            _configFilePathLocator = configFilePathLocator;
        }

        // Methods

        public void Write(IEnumerable<FileNode> fileConfigurations)
        {
            var filePath = _configFilePathLocator.Locate();
            using(var fileStream = new FileStream(filePath.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            using (StreamWriter file = new StreamWriter(fileStream))            
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, fileConfigurations);
            }
        }
    }
}
