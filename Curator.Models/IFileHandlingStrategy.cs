﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public interface IFileHandlingStrategy
    {
        event Action<IFileHandlingStrategy, DeltaFileTransaction> Handled;

        Boolean CanHandle(FileNode node);

        void Handle(FileNode node);

        Task HandleAsync(FileNode node);
    }
}
