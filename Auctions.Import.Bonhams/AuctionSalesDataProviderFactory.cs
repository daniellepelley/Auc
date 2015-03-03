using System;
using System.Security.Cryptography.X509Certificates;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.Model;

namespace Auctions.Import.Bonhams
{
    public class AuctionSalesDataProviderFactory : IAuctionSalesDataProviderFactory
    {
        public IDataProvider<AuctionSale> Create(Func<AuctionListing, bool> filter, IMonitor monitor)
        {
            var jsonWebScraper = new JsonWebScraper<AuctionListing>(new HttpLoader(), new BonhamsAuctionJsonDataExtractor(), monitor);
            var auctionListingProvider = new AuctionListingProvider(filter, jsonWebScraper, new BonhamsAuctionUrlProvider());

            var webScraper = new JsonWebScraper<BonhamsSale>(new HttpLoader(), new BonhamsSaleJsonDataExtractor(), monitor);

            var auctionSalesScraper = new AuctionSalesScraper<BonhamsSale>(webScraper, new BonhamsSalesMapper());

            return new AuctionSalesDataProvider(auctionSalesScraper, auctionListingProvider);
        }
    }
}