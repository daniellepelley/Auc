using System.IO;
using System.Linq;
using Auctions.Import.Coys.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Coys.Test
{
    public class CoysSalesImporterTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var sales = GetSales();
            Assert.AreEqual(75, sales.Count());
        }

        [Test]
        [Category("Unit")]
        public void CurrencyIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("EUR", sales[0].Currency);
        }

        [Test]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual(5317, sales[5].Price);
        }

        [Test]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual(10634, sales[0].Price);
        }

        [Test]
        [Category("Unit")]
        public void YearIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual(1979, sales[0].Year);
        }

        [Test]
        [Category("Unit")]
        public void YearIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual(null, sales[4].Year);
        }

        [Test]
        [Category("Unit")]
        public void MakeIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("Rolls-Royce", sales[34].Make);
        }

        [Test]
        [Category("Unit")]
        public void MakeIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("Cadillac", sales[5].Make);
        }

        [Test]
        [Category("Unit")]
        public void ModelIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("Fleetwood", sales[5].Model);
        }

        [Test]
        [Category("Unit")]
        public void ModelIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("Silver Cloud II", sales[34].Model);
        }

        private static AuctionSale[] GetSales(string htmlFile = "/Html/SalesList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var webScraper = new CoysSalesWebScraper(mockHtmlLoader.Object, new DocumentBuilder());

            var sut = new AuctionSalesScraper<CoysSale>(webScraper, new CoysSalesMapper());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}