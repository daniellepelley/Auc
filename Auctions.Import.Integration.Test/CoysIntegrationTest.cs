using System;
using System.Linq;
using Auctions.Import.Bonhams;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Coys;
using Auctions.Import.Coys.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.Model;
using NUnit.Framework;

namespace Auctions.Import.Integration.Test
{
    public class CoysIntegrationTest
    {
        [Test]
        [Ignore]
        public void Test()
        {
            var auctionListingProvider = new AuctionListingProvider(x => true, new CoysAuctionsWebScraper(), new CoysAuctionUrlProvider());

            var webScraper = new CoysSalesWebScraper();
            var auctionSalesScraper = new AuctionSalesScraper<CoysSale>(webScraper, new CoysSalesMapper());

            var auctionSalesDataProvider = new AuctionSalesDataProvider(auctionSalesScraper, auctionListingProvider);
            var service = new AuctionSalesImportService(auctionSalesDataProvider);

            var results = service.Import().Result;

            var filtered = results.Where(x => x.AuctionListing.Date < new DateTime(2011, 1, 1));
             
            Assert.AreEqual(1000, results.Count());
        }
    }
}