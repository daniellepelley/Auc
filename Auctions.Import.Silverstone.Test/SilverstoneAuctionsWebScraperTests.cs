using System.IO;
using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Silverstone.Test
{
    public class SilverstoneAuctionsWebScraperTests
    {
        [Category("Unit")]
        public void Import()
        {
            var sales = GetAuctions();
            Assert.AreEqual(25, sales.Count());
        }

        [TestCase(0, "Race Retro Classic Car Sale")]
        [TestCase(1, "NEC Classic Motor Show Sale")]
        [Category("Unit")]
        public void NameIsFormattedCorrectly(int index, string expected)
        {
            var sales = GetAuctions();
            Assert.AreEqual(expected, sales[index].Name);
        }

        [TestCase(0, "22/02/2015")]
        [TestCase(1, "16/11/2014")]
        [Category("Unit")]
        public void DateIsFormattedCorrectly(int index, string expected)
        {
            var sales = GetAuctions();
            Assert.AreEqual(expected, sales[index].Date.Value.ToString("dd/MM/yyyy"));
        }

        [TestCase(0, "https://www.silverstoneauctions.com/race-retro--classic-car-sale-2015/view_lots/pn/all")]
        [TestCase(1, "https://www.silverstoneauctions.com/nec-classic-motor-show-sale-2014/view_lots/pn/all")]
        [Category("Unit")]
        public void UrlIsFormattedCorrectly(int index, string expected)
        {
            var sales = GetAuctions();
            Assert.AreEqual(expected, sales[index].Url);
        }

        private static AuctionListing[] GetAuctions(string htmlFile = "/Html/AuctionsList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new SilverstoneAuctionsWebDataImporter(mockHtmlLoader.Object, new DocumentBuilder());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}