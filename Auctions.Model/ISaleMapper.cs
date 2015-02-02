namespace Auctions.Model
{
    public interface ISaleMapper<T>
    {
        Sale Map(T source);
    }
}