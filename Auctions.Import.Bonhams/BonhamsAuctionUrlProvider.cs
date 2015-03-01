using System.Collections.Generic;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.Bonhams
{
    public class BonhamsAuctionUrlProvider : IUrlProvider
    {
        public IEnumerable<string> GetUrls()
        {
            return new[] { ConfigSettings.AuctionsListUrl };
        }
    }
}