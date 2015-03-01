using System;
using System.Linq;
using Auctions.Import.Bonhams;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.Model;
using NUnit.Framework;

namespace Auctions.Import.Integration.Test
{
    public class BonhamsIntegrationTest
    {
        [Test]
        [Ignore]
        public void Test()
        {
            var jsonWebScraper = new JsonWebScraper<AuctionListing>(new HttpLoader(), new BonhamsAuctionJsonDataExtractor());
            var auctionListingProvider = new AuctionListingProvider(x => x.Date >= new DateTime(2014, 1, 1), jsonWebScraper, new BonhamsAuctionUrlProvider());

            var webScraper = new JsonWebScraper<BonhamsSale>(new HttpLoader(), new BonhamsSaleJsonDataExtractor());

            var auctionSalesScraper = new AuctionSalesScraper<BonhamsSale>(webScraper, new BonhamsSalesMapper());

            var auctionSalesDataProvider = new AuctionSalesDataProvider(auctionSalesScraper, auctionListingProvider);
            var service = new AuctionSalesImportService(auctionSalesDataProvider);

            var results = service.Import().Result;
            Assert.IsTrue(results.Any());
        }
    }
}