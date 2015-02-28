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

        [TestCase(287, 1926)]
        [TestCase(283, 1990)]
        [Category("Unit")]
        public void ImportPopulatesYear(int index, int expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales.ToArray().ElementAt(index).Year);
        }

        [TestCase(287, true)]
        [TestCase(283, true)]
        [Category("Unit")]
        public void ImportPopulatesSold1(int index, bool expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales.ToArray().ElementAt(index).Sold);
        }

        [TestCase(287, "Rolls-Royce")]
        [TestCase(283, "Gravetti")]
        [Category("Unit")]
        public void ImportPopulatesMake1(int index, string expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales.ToArray().ElementAt(index).Make);
        }

        [TestCase(287, 27833)]
        [TestCase(283, 23000)]
        [Category("Unit")]
        public void ImportPopulatesPrice1(int index, int expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales.ToArray().ElementAt(index).Price);
        }

        [TestCase(287, "20hp Six-Light Saloon")]
        [TestCase(283, "Cobra 427 Replica Roadster")]
        [Category("Unit")]
        public void ImportPopulatesModel1(int index, string expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales.ToArray().ElementAt(index).Model);
        }

        private static AuctionSale[] GetSales(string jsonFile = "/Json/AuctionSalesList.json")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + jsonFile));

            var scraper = new JsonWebScraper<BonhamsSale>(mockHtmlLoader.Object, new BonhamsSaleJsonDataExtractor());

            var sut = new AuctionSalesScraper<BonhamsSale>(scraper, new BonhamsSalesMapper());
            
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}