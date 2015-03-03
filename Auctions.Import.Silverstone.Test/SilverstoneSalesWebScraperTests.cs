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

        [TestCase(1, "£18,000")]
        [TestCase(2, "£18,900")]
        [Category("Unit")]
        public void PriceIsFormattedCorrectly(int index, string expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Price);
        }

        [TestCase(1, "1971 Ford Escort Twin-Cam")]
        [TestCase(2, "1968 MGC Roadster")]
        [Category("Unit")]
        public void DescriptionIsFormattedCorrectly(int index, string expected)
        {
            var sales = GetSales();
            Assert.AreEqual(expected, sales[index].Description);
        }

        private static SilverstoneSale[] GetSales(string htmlFile = "/Html/SalesList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new SilverstoneSalesWebDataImporter(mockHtmlLoader.Object, new DocumentBuilder());
            var sales =
                sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx")
                    .Result;
            return sales;
        }
    }
}
