namespace Curator.Models
{
    public interface ISignatureCreator
    {
        byte[] CreateSignature(FileNode node);
    }
}