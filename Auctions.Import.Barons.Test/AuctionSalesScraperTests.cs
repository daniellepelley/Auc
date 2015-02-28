using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Auctions.Import.Barons.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Barons.Test
{
    public class AuctionSalesScraperTests
    {
        private static AuctionSale[] GetSales(string htmlFile = "/Html/BaronsAuctionHistoryHtml.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var webScraper = new BaronsSalesWebScraper(mockHtmlLoader.Object, new DocumentBuilder());

            var sut = new AuctionSalesScraper<BaronsSale>(webScraper, new BaronsSalesMapperBase());

            var sales = sut.Import("http://www.barons-auctions.com/fullresults.php");
            return sales.Result;
        }

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

        [Test]
        [Category("Unit")]
        public void YearIsCorrectlyImported()
        {
            var sales = GetSales();
            Assert.AreEqual(1970, sales[24].Year);
        }

        [Test]
        [Category("Unit")]
        public void YearIsCorrectlyImported2()
        {
            var sales = GetSales();
            Assert.AreEqual(1976, sales[25].Year);
        }

        [Test]
        [Category("Unit")]
        public void MakeIsCorrectlyImported()
        {
            var sales = GetSales();
            Assert.AreEqual("Alfa Romeo", sales[24].Make);
        }

        [Test]
        [Category("Unit")]
        public void MakeIsCorrectlyImported2()
        {
            var sales = GetSales();
            Assert.AreEqual("Alfa Romeo", sales[25].Make);
        }

        [Test]
        [Category("Unit")]
        public void ModelIsCorrectlyImported()
        {
            var sales = GetSales();
            Assert.AreEqual("1300 Giulia GTA Stepfront", sales[24].Model);
        }

        [Test]
        [Category("Unit")]
        public void ModelIsCorrectlyImported2()
        {
            var sales = GetSales();
            Assert.AreEqual("1600 GT Junior", sales[25].Model);
        }

        [Test]
        [Category("Unit")]
        public void PriceIsCorrectlyImported()
        {
            var sales = GetSales();
            Assert.AreEqual(13500, sales[24].Price);
        }

        [Test]
        [Category("Unit")]
        public void PriceIsCorrectlyImported2()
        {
            var sales = GetSales();
            Assert.AreEqual(5100, sales[25].Price);
        }
    }


}