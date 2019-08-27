namespace Curator.Models
{
    public interface ITransactionWriter
    {
        void Write(DeltaFileTransaction transaction);
    }
}