using System;

namespace Curator.Models
{
    public interface IDeltaCreator
    {
        Byte[] BuildDelta(Byte[] signature, FileNode node);

        Byte[] BuildDelta(Byte[] signature, Byte[] data);
    }
}