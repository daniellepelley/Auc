using System.Collections.Generic;
using System.IO;
using System.Linq;
using Auctions.Import.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class HAndHSalesWebScraperTests
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
        public void PriceIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("Sold For £4,928", sales[1].Price);
        }

        [Test]
        [Category("Unit")]
        public void PriceIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("Withdrawn", sales[6].Price);
        }

        [Test]
        [Category("Unit")]
        public void DescriptionIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("1985 Land Rover 90 2.5 Diesel", sales[1].Description);
        }

        [Test]
        [Category("Unit")]
        public void DescriptionIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("1948 Morgan 4-4", sales[6].Description);
        }

        private static HAndHSale[] GetSales(string htmlFile = "/Html/PageHtml.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new HAndHSalesWebScraper(mockHtmlLoader.Object, new DocumentBuilder());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }

        private static IEnumerable<HAndHSale> GetSalesFromEndPage()
        {
            return GetSales("/Html/PageHtml2.txt");
        }

        private static IEnumerable<HAndHSale> GetSalesFromEmptyPage()
        {
            return GetSales("/Html/PageHtmlEmpty.txt");
        }
    }
}
