namespace Auctions.Import.Infrastructure
{
    public interface IWebScraper<T>
    {
        T[] Import(string url);
    }
}