using System.IO;
using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Import.Silverstone.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Silverstone.Test
{
    public class SilverstoneSalesWebScraperTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var sales = GetSales();
            Assert.AreEqual(256, sales.Count());
        }

        [Test]
        [Category("Unit")]
        public void PriceIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("£18,000", sales[1].Price);
        }

        [Test]
        [Category("Unit")]
        public void PriceIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("£18,900", sales[2].Price);
        }

        [Test]
        [Category("Unit")]
        public void DescriptionIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("1971 Ford Escort Twin-Cam", sales[1].Description);
        }

        [Test]
        [Category("Unit")]
        public void DescriptionIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("1968 MGC Roadster", sales[2].Description);
        }

        private static SilverstoneSale[] GetSales(string htmlFile = "/Html/SalesList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new SilverstoneSalesWebScraper(mockHtmlLoader.Object, new DocumentBuilder());
            var sales =
                sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx")
                    .Result;
            return sales;
        }
    }
}
