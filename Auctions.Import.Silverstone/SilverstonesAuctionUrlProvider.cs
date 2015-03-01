using System.Collections.Generic;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.Silverstone
{
    public class SilverstonesAuctionUrlProvider : IUrlProvider
    {
        public IEnumerable<string> GetUrls()
        {
            return ConfigSettings.AuctionsListUrls;
        }
    }
}