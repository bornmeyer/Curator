using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public class FileSelectService : IFileSelectService
    {
        // Events

        public event Action<FileInfo> FileSelected;

        // Methods

        public void OpenDialogue()
        {
            var fileDialogue = new OpenFileDialog();
            if (fileDialogue.ShowDialog() == true)
            {
                var fullPath = fileDialogue.FileName;
                var fileInfo = new FileInfo(fullPath);
                OnFileSelected(fileInfo);
            }
        }

        private void OnFileSelected(FileInfo fileInfo)
        {
            FileSelected?.Invoke(fileInfo);
        }
    }
}