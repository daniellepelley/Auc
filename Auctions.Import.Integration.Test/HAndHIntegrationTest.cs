using System;
using System.Linq;
using Auctions.Import.HAndH;
using Auctions.Import.HAndH.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.DomainModel;
using NUnit.Framework;

namespace Auctions.Import.Integration.Test
{
    public class HAndHIntegrationTest
    {
        [Test]
        [Ignore]
        public void Test()
        {
            var auctionListingProvider = new AuctionListingProvider(x => x.Date >= new DateTime(2009, 1, 1),
                new HAndHAuctionListingsWebDataImporter(),
                new HAndHAuctionUrlProvider("http://www.classic-auctions.com/auctions/previous.aspx?year={0}"));

            var webScraper = new HAndHSalesWebDataImporter();

            var auctionSalesScraper = new AuctionSalesDataImporter<HAndHSale>(webScraper, new HandHSalesMapper());

            var auctionSalesDataProvider = new AuctionSalesDataProvider(auctionSalesScraper, auctionListingProvider);
            var service = new AuctionSalesImportService(auctionSalesDataProvider);

            AuctionSale[] results = service.Import().Result;

            Assert.AreEqual(1000, results.Count());
        }

        [Test]
        public void HAndHAuctionSalesDataProviderFactoryBuildsCorrectly()
        {
            var factory = new HAndHAuctionSalesDataProviderFactory();

            factory.Create(x => true, new Monitor(x => { }));
        }
    }
}