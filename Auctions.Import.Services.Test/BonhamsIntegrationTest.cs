using System;
using System.Linq;
using Auctions.Import.Barons;
using Auctions.Import.Barons.Model;
using Auctions.Import.Bonhams;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using NUnit.Framework;

namespace Auctions.Import.Services.Test
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

    public class BaronsIntegrationTest
    {
        [Test]
        [Ignore]
        public void Test()
        {
            var auctionSalesScraper = new AuctionSalesScraper<BaronsSale>(new BaronsSalesWebScraper(), new BaronsSalesMapper());

            var auctionSalesDataProvider = new AuctionSalesDataProvider(auctionSalesScraper, new BaronsAuctionListingProvider());
            var service = new AuctionSalesImportService(auctionSalesDataProvider);

            var results = service.Import().Result;
            Assert.IsTrue(results.Any());
        }
    }
}