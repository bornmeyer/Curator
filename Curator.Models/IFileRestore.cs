namespace Curator.Models
{
    public interface IFileRestore
    {
        byte[] Restore(FileNode node, LogEntry entry);
    }
}