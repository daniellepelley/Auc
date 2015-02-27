using System.Collections.Generic;
using System.Linq;
using Auctions.Model;
using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class IntegrationTests
    {
        [Test]
        [Ignore]
        [Category("Integration")]
        public void PageImporterImport()
        {
            var sut = new HAndHSalesWebScraper();
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;

            Assert.AreEqual(12, sales.Count());
        }

        [Test]
        [Ignore]
        [Category("Integration")]
        public void AuctionsImport()
        {
            var sales = new List<AuctionSale>();
            
            var baseUrl = "http://www.classic-auctions.com/auctions/previous.aspx?year={0}";

            var salesImporter = new AuctionSalesScraper<HAndHSale>(new HAndHSalesWebScraper(), new HandHSalesMapper());

            var auctionImporter = new AuctionImporter(salesImporter);
            var auctionListingImporter = new HAndHAuctionListingsWebScraper();

            for (int i = 2010; i < 2015; i++)
            {
                var auctionListingsUrl = string.Format(baseUrl, i);

                var auctionListings = auctionListingImporter.Import(auctionListingsUrl).Result;

                foreach (var auctionListing in auctionListings.Where(x => x.Type == "Car").ToArray())
                {
                    var auctionUrl = "http://www.classic-auctions.com/" + auctionListing.Url + "?p={0}";
                    var auction = auctionImporter.Import(auctionUrl);
                    sales.AddRange(auction.Sales);
                }
            }

            var triumph = sales
                .Where(x => x.Make == "Triumph")
                .OrderBy(x => x.Price)
                .GroupBy(x => x.Model);

            Assert.IsTrue(sales.Count() > 100);
        }

    }
}