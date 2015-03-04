using System.IO;
using Auctions.Import.Infrastructure;
using Auctions.DomainModel;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class AuctionListingImporterTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var auction = GetAuctionListings();
            Assert.AreEqual(19, auction.Length);
        }

        [TestCase(0, "Chateau Impney")]
        [TestCase(1, "The Pavilion Gardens")]
        [Category("Unit")]
        public void NameIsFormattedCorrectly(int index, string expected)
        {
            var auctions = GetAuctionListings();
            Assert.AreEqual(expected, auctions[index].Name);
        }

        [TestCase(0, "04/12/2013")]
        [TestCase(1, "30/10/2013")]
        [Category("Unit")]
        public void DateIsFormattedCorrectly(int index, string expected)
        {
            var auctions = GetAuctionListings();
            Assert.AreEqual(expected, auctions[index].Date.Value.ToString("dd/MM/yyyy"));
        }

        [TestCase(0, "http://www.classic-auctions.com/Auctions/04-12-2013-ChateauImpney-1346.aspx")]
        [TestCase(1, "http://www.classic-auctions.com/Auctions/30-10-2013-ThePavilionGardens-1345.aspx")]
        [Category("Unit")]
        public void UrlIsFormattedCorrectly(int index, string expected)
        {
            var auctions = GetAuctionListings();
            Assert.AreEqual(expected, auctions[index].Url);
        }

        private static AuctionListing[] GetAuctionListings()
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/AuctionListingHtml.txt"));

            var sut = new HAndHAuctionListingsWebDataImporter(mockHtmlLoader.Object, new DocumentBuilder());
            return sut.Import("http://www.classic-auctions.com/auctions/previous.aspx?year=2013").Result;
        }
    }
}