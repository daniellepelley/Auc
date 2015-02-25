namespace Auctions.Model
{
    public interface ISalesImporter
    {
        AuctionSale[] Import(string url);
    }
}