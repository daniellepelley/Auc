namespace Auctions.Import.HAndH
{
    public interface IAuctionImporter
    {
        Auction Import(string baseUrl);
    }
}