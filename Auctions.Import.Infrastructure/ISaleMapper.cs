namespace Auctions.Import.Infrastructure
{
    public interface ISaleMapper<T>
    {
        Sale Map(T source);
    }
}