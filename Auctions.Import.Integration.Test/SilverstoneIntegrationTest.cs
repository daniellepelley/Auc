using System;
using System.Linq;
using Auctions.Import.Services;
using Auctions.Import.Silverstone;
using Auctions.Import.Silverstone.Model;
using Auctions.Model;
using NUnit.Framework;

namespace Auctions.Import.Integration.Test
{
    public class SilverstoneIntegrationTest
    {
        [Test]
        [Ignore]
        public void Test()
        {
            var auctionListingProvider = new AuctionListingProvider(x => x.Date >= new DateTime(2009, 1, 1), new SilverstoneAuctionsWebDataImporter(), new SilverstonesAuctionUrlProvider());

            var webScraper = new SilverstoneSalesWebDataImporter();
             
            var auctionSalesScraper = new AuctionSalesDataImporter<SilverstoneSale>(webScraper, new SilverstoneSalesMapper());

            var auctionSalesDataProvider = new AuctionSalesDataProvider(auctionSalesScraper, auctionListingProvider);
            var service = new AuctionSalesImportService(auctionSalesDataProvider);

            var results = service.Import().Result;

            Assert.AreEqual(1000, results.Count());
        }
    }
}