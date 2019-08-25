namespace Curator.Models
{
    public interface IDeltaPatcher
    {
        byte[] Patch(byte[] content, byte[] delta);
    }
}