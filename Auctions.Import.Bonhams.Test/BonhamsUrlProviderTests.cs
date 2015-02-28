using System;
using System.IO;
using System.Linq;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Bonhams.Test
{
    public class BonhamsUrlProviderTests
    {
        [Test]
        [Category("Unit")]
        public void GetUrlsImportsUrlsTheCorrectNumberOfUrlsAsDeterminedByTheFilter()
        {
            var urls = GetUrls();
            Assert.AreEqual(2, urls.Count());
        }

        [Test]
        [Category("Unit")]
        public void GetUrlsImportsUrlsInTheCorrectFormat1()
        {
            var urls = GetUrls();
            Assert.AreEqual("https://www.bonhams.com/api/v1/lots/22528/?category=results&length=540&minimal=false&page=1", urls[0]);
        }

        [Test]
        [Category("Unit")]
        public void GetUrlsImportsUrlsInTheCorrectFormat2()
        {
            var urls = GetUrls();
            Assert.AreEqual("https://www.bonhams.com/api/v1/lots/22205/?category=results&length=540&minimal=false&page=1", urls[1]);
        }

        private static string[] GetUrls(string jsonFile = "/Json/AuctionsList.json")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + jsonFile));

            var scraper = new JsonWebScraper<BonhamsAuction>(mockHtmlLoader.Object, new BonhamsAuctionJsonDataExtractor());

            var sut = new BonhamsUrlProvider(x => x.Date >= new DateTime(2015, 1, 1), scraper, "foo");

            return sut.GetUrls().Result;
        }
    }
}