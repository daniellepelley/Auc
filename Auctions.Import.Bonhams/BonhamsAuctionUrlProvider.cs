using Auctions.Model;

namespace Auctions.Import.Bonhams
{
    public class BonhamsAuctionUrlProvider : IUrlProvider
    {
        public string[] GetUrls()
        {
            return new[] { ConfigSettings.AuctionsListUrl };
        }
    }
}