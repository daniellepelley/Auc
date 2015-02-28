using System;
using System.Linq;
using System.Threading.Tasks;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.Bonhams
{
    public class BonhamsAuctionListingProvider : IAuctionListingProvider
    {
        private readonly IWebScraper<AuctionListing> _webScraper;
        private readonly string _auctionsListUrl;
        private readonly Func<AuctionListing, bool> _filter;

        public BonhamsAuctionListingProvider(Func<AuctionListing, bool> filter, IWebScraper<AuctionListing> webScraper, string auctionsListUrl)
        {
            _filter = filter;
            _auctionsListUrl = auctionsListUrl;
            _webScraper = webScraper;
        }

        public async Task<AuctionListing[]> GetAuctionListings()
        {
            var auctions = await _webScraper.Import(_auctionsListUrl);

            return  auctions.Where(_filter).ToArray();
        }
    }
}