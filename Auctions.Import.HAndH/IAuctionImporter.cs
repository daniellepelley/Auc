using Auctions.Import.HAndH.Model;

namespace Auctions.Import.HAndH
{
    public interface IAuctionImporter
    {
        HAndHAuction Import(string baseUrl);
    }
}