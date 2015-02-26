using System.IO;
using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class HAndHSalesImporterTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var sales = GetSales();
            Assert.AreEqual(12, sales.Count());
        }

        [Test]
        [Category("Unit")]
        public void ImportEndPage()
        {
            var sales = GetSalesFromEndPage();
            Assert.AreEqual(3, sales.Count());
        }

        [Test]
        [Category("Unit")]
        public void ImportEmptyPage()
        {
            var sales = GetSalesFromEmptyPage();
            Assert.AreEqual(0, sales.Count());
        }

        [Test]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual(2464, sales[0].Price);
        }

        [Test]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual(4928, sales[1].Price);
        }

        [Test]
        [Category("Unit")]
        public void SalePriceIsFormattedCorrectly3()
        {
            var sales = GetSales();
            Assert.AreEqual(null, sales[6].Price);
        }

        [Test]
        [Category("Unit")]
        public void YearIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual(1960, sales[0].Year);
        }

        [Test]
        [Category("Unit")]
        public void YearIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual(1985, sales[1].Year);
        }

        [Test]
        [Category("Unit")]
        public void MakeIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("Rolls-Royce", sales[0].Make);
        }

        [Test]
        [Category("Unit")]
        public void MakeIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("Land Rover", sales[1].Make);
        }

        [Test]
        [Category("Unit")]
        public void ModelIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("Silver Cloud II", sales[0].Model);
        }

        [Test]
        [Category("Unit")]
        public void ModelIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("90 2.5 Diesel", sales[1].Model);
        }

        private static AuctionSale[] GetSales(string htmlFile = "/Html/PageHtml.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var webScraper = new HAndHSalesWebScraper(mockHtmlLoader.Object, new DocumentBuilder());

            var sut = new AuctionSalesScraper<HAndHSale>(webScraper, new HandHSalesMapper());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }

        private static AuctionSale[] GetSalesFromEndPage()
        {
            return GetSales("/Html/PageHtml2.txt");
        }

        private static AuctionSale[] GetSalesFromEmptyPage()
        {
            return GetSales("/Html/PageHtmlEmpty.txt");
        }
    }
}