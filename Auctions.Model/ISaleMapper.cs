namespace Auctions.Model
{
    public interface ISaleMapper<T>
    {
        AuctionSale Map(T source);
    }
}