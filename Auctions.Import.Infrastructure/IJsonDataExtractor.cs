namespace Auctions.Import.Infrastructure
{
    public interface IJsonDataExtractor<out T>
    {
        T[] Extract(string data);
    }
}