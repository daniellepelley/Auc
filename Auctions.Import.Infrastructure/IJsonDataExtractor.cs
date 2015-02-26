namespace Auctions.Import.Infrastructure
{
    public interface IJsonDataExtractor<T>
    {
        T[] Extract(string data);
    }
}