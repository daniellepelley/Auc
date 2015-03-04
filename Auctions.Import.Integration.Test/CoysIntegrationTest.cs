using System.Linq;
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
            var auctionListingProvider = new AuctionListingProvider(x => true, new CoysAuctionsWebDataImporter(), new CoysAuctionUrlProvider());

            var webScraper = new CoysSalesWebDataImporter();
            var auctionSalesScraper = new AuctionSalesDataImporter<CoysSale>(webScraper, new CoysSalesMapper());

            var auctionSalesDataProvider = new AuctionSalesDataProvider(auctionSalesScraper, auctionListingProvider);
            var service = new AuctionSalesImportService(auctionSalesDataProvider);

            var results = service.Import().Result;

            Assert.AreEqual(1000, results.Count());
        }

        [Test]
        public void CoysAuctionSalesDataProviderFactoryBuildsCorrectly()
        {
            var factory = new CoysAuctionSalesDataProviderFactory();

            factory.Create(x => true, new Monitor(x => { }));
        }
    }
}