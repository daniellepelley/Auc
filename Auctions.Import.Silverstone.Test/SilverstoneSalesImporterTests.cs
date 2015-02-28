using System.IO;
using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Import.Silverstone.Model;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Silverstone.Test
{
    public class SilverstoneSalesImporterTests
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
        public void CurrencyIsFormattedCorrectly()
        {
            var sales = GetSales();
            Assert.AreEqual("GBP", sales[0].Currency);
        }

        [TestCase(0, null)]
        [TestCase(1, 18000)]
        [TestCase(2, 18900)]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly(int index, int? expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Price);
        }

        [TestCase(1, 1971)]
        [TestCase(2, 1968)]
        [Category("Unit")]
        public void YearIsFormattedCorrectly(int index, int? expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Year);
        }

        [TestCase(1, "Ford")]
        [TestCase(2, "MGC")]
        [Category("Unit")]
        public void MakeIsFormattedCorrectly(int index, string expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Make);
        }

        [TestCase(1, "Escort Twin-Cam")]
        [TestCase(2, "Roadster")]
        [Category("Unit")]
        public void ModelIsFormattedCorrectly(int index, string expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Model);
        }

        private static AuctionSale[] GetSales(string htmlFile = "/Html/SalesList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var webScraper = new SilverstoneSalesWebScraper(mockHtmlLoader.Object, new DocumentBuilder());

            var sut = new AuctionSalesScraper<SilverstoneSale>(webScraper, new SilverstoneSalesMapperBase());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}