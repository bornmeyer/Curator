namespace Curator.Models
{
    public interface IDeltaCreator
    {
        byte[] BuildDelta(byte[] signature, FileNode node);
    }
}