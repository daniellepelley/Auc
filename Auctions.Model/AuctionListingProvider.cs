using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;

namespace Auctions.Model
{
    public class AuctionListingProvider : IAuctionListingProvider
    {
        private readonly IWebDataImporter<AuctionListing> _webDataImporter;
        private readonly Func<AuctionListing, bool> _filter;
        private readonly IUrlProvider _urlProvider;

        public AuctionListingProvider(Func<AuctionListing, bool> filter, IWebDataImporter<AuctionListing> webDataImporter, IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
            _filter = filter;
            _webDataImporter = webDataImporter;
        }

        public async Task<AuctionListing[]> GetAuctionListings()
        {
            var auctionListings = new List<AuctionListing>();
            foreach (var url in _urlProvider.GetUrls())
            {
                auctionListings.AddRange(await _webDataImporter.Import(url));
            }
            return auctionListings.Where(_filter).ToArray();
        }
    }
}