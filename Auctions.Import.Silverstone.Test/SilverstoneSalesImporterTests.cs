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
        public void CurrencyIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("GBP", sales[0].Currency);
        }

        [Test]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual(18000, sales[1].Price);
        }

        [Test]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual(18900, sales[2].Price);
        }

        [Test]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly3()
        {
            var sales = GetSales();
            Assert.AreEqual(null, sales[0].Price);
        }

        [Test]
        [Category("Unit")]
        public void YearIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual(1971, sales[1].Year);
        }

        [Test]
        [Category("Unit")]
        public void YearIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual(1968, sales[2].Year);
        }

        [Test]
        [Category("Unit")]
        public void MakeIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("Ford", sales[1].Make);
        }

        [Test]
        [Category("Unit")]
        public void MakeIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("MGC", sales[2].Make);
        }

        [Test]
        [Category("Unit")]
        public void ModelIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("Escort Twin-Cam", sales[1].Model);
        }

        [Test]
        [Category("Unit")]
        public void ModelIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("Roadster", sales[2].Model);
        }

        private static AuctionSale[] GetSales(string htmlFile = "/Html/SalesList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var webScraper = new SilverstoneSalesWebScraper(mockHtmlLoader.Object, new DocumentBuilder());

            var sut = new AuctionSalesScraper<SilverstoneSale>(webScraper, new SilverstoneSalesMapper());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}