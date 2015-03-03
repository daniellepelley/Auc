namespace Auctions.Import.Infrastructure
{
    public interface IMonitor
    {
        void Update(string message);
    }
}