using System.Linq;
using Auctions.Import.Barons;
using Auctions.Import.Barons.Model;
using Auctions.Import.Services;
using Auctions.Model;
using NUnit.Framework;

namespace Auctions.Import.Integration.Test
{
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