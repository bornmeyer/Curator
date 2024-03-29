﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class ConfigFilePathLocator : IConfigFilePathLocator
    {
        // Fields

        private readonly String _folderPath = String.Empty;

        // Constructors

        public ConfigFilePathLocator()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _folderPath = Path.Combine(appDataFolder, Constants.AppFolder);
        }

        // Methods

        public FileInfo Locate()
        {
            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);
            var filePath = Path.Combine(_folderPath, Constants.ConfigFileName);
            if (!File.Exists(filePath))
                File.Create(filePath);

            return new FileInfo(filePath);
        }
    }
}
