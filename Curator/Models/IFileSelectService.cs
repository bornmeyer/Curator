using System;
using System.IO;

namespace Curator.Models
{
    public interface IFileSelectService
    {
        event Action<FileInfo> FileSelected;

        void OpenDialogue();
    }
}