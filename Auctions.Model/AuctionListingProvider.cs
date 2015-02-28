using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;

namespace Auctions.Model
{
    public class AuctionListingProvider : IAuctionListingProvider
    {
        private readonly IWebScraper<AuctionListing> _webScraper;
        private readonly Func<AuctionListing, bool> _filter;
        private readonly IUrlProvider _urlProvider;

        public AuctionListingProvider(Func<AuctionListing, bool> filter, IWebScraper<AuctionListing> webScraper, IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
            _filter = filter;
            _webScraper = webScraper;
        }

        public async Task<AuctionListing[]> GetAuctionListings()
        {
            var auctionListings = new List<AuctionListing>();
            foreach (var url in _urlProvider.GetUrls())
            {
                auctionListings.AddRange(await _webScraper.Import(url));
            }
            return auctionListings.Where(_filter).ToArray();
        }
    }
}