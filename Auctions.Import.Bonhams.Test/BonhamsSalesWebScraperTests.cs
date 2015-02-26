using System.IO;
using System.Linq;
using Auctions.Import.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Bonhams.Test
{
    public class BonhamsSalesWebScraperTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var sales = GetSales();
            Assert.IsTrue(sales.Any());
            Assert.AreEqual("Assorted books and literature relating to the Bentley marque, ((Qty))", sales.First().Description);
            Assert.AreEqual("625", sales.First().Price);
            Assert.AreEqual("£", sales.First().Currency);
        }

        private static BonhamsSale[] GetSales(string jsonFile = "/Json/AuctionSalesList.json")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + jsonFile));

            var sut = new BonhamsSalesWebScraper(mockHtmlLoader.Object, new BonhamsJsonDataExtractor());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}
