using System;

namespace Curator.Models
{
    public interface ISignatureCreator
    {
       Byte[] CreateSignature(FileNode node);
       Byte[] CreateSignature(Byte[] data);
    }
}