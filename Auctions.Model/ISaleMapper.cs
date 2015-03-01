namespace Auctions.Model
{
    public interface ISaleMapper<in T>
    {
        AuctionSale Map(T source);
    }
}