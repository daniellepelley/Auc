namespace Auctions.DomainModel
{
    public interface ISaleMapper<in T>
    {
        AuctionSale Map(T source);
    }
}