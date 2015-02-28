using System.IO;
using System.Linq;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Bonhams.Test
{
    public class BonhamsSalesImporterTests
    {
        [Test]
        [Category("Unit")]
        public void CurrencyIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("GBP", sales[0].Currency);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesYear1()
        {
            var sales = GetSales();
            Assert.AreEqual(1926, sales.ToArray().ElementAt(287).Year);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesYear2()
        {
            var sales = GetSales();
            Assert.AreEqual(1990, sales.ToArray().ElementAt(283).Year);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesSold1()
        {
            var sales = GetSales();
            Assert.AreEqual(true, sales.ToArray().ElementAt(287).Sold);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesSold2()
        {
            var sales = GetSales();
            Assert.AreEqual(true, sales.ToArray().ElementAt(283).Sold);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesMake1()
        {
            var sales = GetSales();
            Assert.AreEqual("Rolls-Royce", sales.ToArray().ElementAt(287).Make);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesMake2()
        {
            var sales = GetSales();
            Assert.AreEqual("Gravetti", sales.ToArray().ElementAt(283).Make);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesPrice1()
        {
            var sales = GetSales();
            Assert.AreEqual(27833, sales.ToArray().ElementAt(287).Price);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesPrice2()
        {
            var sales = GetSales();
            Assert.AreEqual(23000, sales.ToArray().ElementAt(283).Price);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesModel1()
        {
            var sales = GetSales();
            Assert.AreEqual("20hp Six-Light Saloon", sales.ToArray().ElementAt(287).Model);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesModel2()
        {
            var sales = GetSales();
            Assert.AreEqual("Cobra 427 Replica Roadster", sales.ToArray().ElementAt(283).Model);
        }

        private static AuctionSale[] GetSales(string jsonFile = "/Json/AuctionSalesList.json")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + jsonFile));

            var scraper = new BonhamsSalesWebScraper(mockHtmlLoader.Object, new BonhamsSaleJsonDataExtractor());

            var sut = new AuctionSalesScraper<BonhamsSale>(scraper, new BonhamsSalesMapperBase());
            
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}