using System.IO;
using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Coys.Test
{
    public class CoysAuctionsWebScraperTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var auctions = GetAuctions();
            Assert.AreEqual(33, auctions.Count());
        }

        [TestCase(0, "The Brundza Collection, Maastricht")]
        [TestCase(1, "Autosport, NEC")]
        [Category("Unit")]
        public void NameIsFormattedCorrectly(int index, string expected)
        {
            var auctions = GetAuctions();
            Assert.AreEqual(expected, auctions[index].Name);
        }

        [TestCase(0, "10/01/2015")]
        [TestCase(7, "09/05/2014")]
        [Category("Unit")]
        public void DateIsFormattedCorrectly(int index, string expected)
        {
            var auctions = GetAuctions();
            Assert.AreEqual(expected, auctions[index].Date.Value.ToString("dd/MM/yyyy"));
        }

        [TestCase(0, "http://www.coys.co.uk/past-auctions.php?auctionID=51")]
        [TestCase(4, "http://www.coys.co.uk/past-auctions.php?auctionID=47")]
        [Category("Unit")]
        public void UrlIsFormattedCorrectly(int index, string expected)
        {
            var auctions = GetAuctions();
            Assert.AreEqual(expected, auctions[index].Url);
        }

        [TestCase(2, "http://www.coys.co.uk/A205results.php")]
        [TestCase(4, "http://www.coys.co.uk/A203results.php")]
        [Category("Unit")]
        public void UrlIsFormattedCorrectlyForAuctionsOld(int index, string expected)
        {
            var auctions = GetAuctionsOld();
            Assert.AreEqual(expected, auctions[index].Url);
        }

        private static AuctionListing[] GetAuctions(string htmlFile = "/Html/AuctionsList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new CoysAuctionsWebScraper(mockHtmlLoader.Object, new DocumentBuilder());
            var auctions = sut.Import("foo").Result;
            return auctions;
        }

        private static AuctionListing[] GetAuctionsOld()
        {
            return GetAuctions("/Html/AuctionsListOld.txt");
        }

    }
}