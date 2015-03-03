using System.IO;
using System.Linq;
using Auctions.Import.Barons.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Barons.Test
{
    public class AuctionSalesScraperTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var sales = GetSales();
            Assert.AreEqual(3298, sales.Count());
        }

        [Test]
        [Category("Unit")]
        public void CurrencyIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("GBP", sales[0].Currency);
        }

        [TestCase(24, 1970)]
        [TestCase(25, 1976)]
        [Category("Unit")]
        public void YearIsCorrectlyImported(int index, int expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Year);
        }

        [TestCase(24, "Alfa Romeo")]
        [TestCase(164, "Audi")]
        [Category("Unit")]
        public void MakeIsCorrectlyImported(int index, string expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Make);
        }

        [TestCase(24, "1300 Giulia GTA Stepfront")]
        [TestCase(25, "1600 GT Junior")]
        [Category("Unit")]
        public void ModelIsCorrectlyImported(int index, string expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Model);
        }

        [TestCase(24, 13500)]
        [TestCase(25, 5100)]
        [Category("Unit")]
        public void PriceIsCorrectlyImported(int index, int expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Price);
        }

        private static AuctionSale[] GetSales(string htmlFile = "/Html/BaronsAuctionHistoryHtml.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var webScraper = new BaronsSalesWebDataImporter(mockHtmlLoader.Object, new DocumentBuilder());

            var sut = new AuctionSalesDataImporter<BaronsSale>(webScraper, new BaronsSalesMapper());

            var sales = sut.Import("http://www.barons-auctions.com/fullresults.php");
            return sales.Result;
        }
    }
}