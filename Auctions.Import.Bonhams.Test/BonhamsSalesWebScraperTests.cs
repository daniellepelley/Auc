using System.Collections.Generic;
using System.IO;
using System.Linq;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Bonhams.Test
{
    public class BonhamsSalesWebScraperTests
    {
        [Test]
        [Category("Unit")]
        public void ImportPopulatesDescription()
        {
            var sales = GetSales();
            Assert.AreEqual("Assorted books and literature relating to the Bentley marque, ((Qty))", sales.First().Description);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesPrice()
        {
            var sales = GetSales();
            Assert.AreEqual("625", sales.First().Price);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesCurrency()
        {
            var sales = GetSales();
            Assert.AreEqual("£", sales.First().Currency);
        }

        [Test]
        [Category("Unit")]
        public void ImportPopulatesLotStatus()
        {
            var sales = GetSales();
            Assert.AreEqual("SOLD", sales.First().LotStatus);
        }

        private static IEnumerable<BonhamsSale> GetSales(string jsonFile = "/Json/AuctionSalesList.json")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + jsonFile));

            var sut = new JsonWebDataImporter<BonhamsSale>(mockHtmlLoader.Object, new BonhamsSaleJsonDataExtractor(), Mock.Of<IMonitor>());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}
