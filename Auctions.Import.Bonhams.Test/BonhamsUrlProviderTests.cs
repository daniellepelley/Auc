using System;
using System.IO;
using System.Linq;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Bonhams.Test
{
    public class BonhamsAuctionListingProviderTests
    {
        [Test]
        [Category("Unit")]
        public void GetAuctionListingsImportsAuctionListingsAndTheCorrectNumberOfUrlsAsDeterminedByTheFilter()
        {
            var auctionListings = GetAuctionListings();
            Assert.AreEqual(2, auctionListings.Count());
        }

        [TestCase(0, "https://www.bonhams.com/api/v1/lots/22528/?category=results&length=540&minimal=false&page=1")]
        [TestCase(1, "https://www.bonhams.com/api/v1/lots/22205/?category=results&length=540&minimal=false&page=1")]
        [Category("Unit")]
        public void GetUrlsImportsUrlsInTheCorrectFormat1(int index, string expected)
        {
            var auctionListings = GetAuctionListings();
            Assert.AreEqual(expected, auctionListings[index].Url);
        }

        private static AuctionListing[] GetAuctionListings(string jsonFile = "/Json/AuctionsList.json")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + jsonFile));

            var scraper = new JsonWebScraper<AuctionListing>(mockHtmlLoader.Object, new BonhamsAuctionJsonDataExtractor());

            var sut = new BonhamsAuctionListingProvider(x => x.Date >= new DateTime(2015, 1, 1), scraper, "foo");

            return sut.GetAuctionListings().Result;
        }
    }
}