using System.IO;
using System.Linq;
using Auctions.Import.Coys.Model;
using Auctions.Import.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Coys.Test
{
    public class CoysSalesWebScraperTests
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
        public void PriceIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("€5,317", sales[5].Price);
        }

        [Test]
        [Category("Unit")]
        public void PriceIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("€354", sales[1].Price);
        }

        [Test]
        [Category("Unit")]
        public void DescriptionIsFormattedCorrectly1()
        {
            var sales = GetSales();
            Assert.AreEqual("1972 Cadillac Fleetwood", sales[5].Description);
        }

        [Test]
        [Category("Unit")]
        public void DescriptionIsFormattedCorrectly2()
        {
            var sales = GetSales();
            Assert.AreEqual("Jawa 350", sales[1].Description);
        }

        private static CoysSale[] GetSales(string htmlFile = "/Html/SalesList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new CoysSalesWebScraper(mockHtmlLoader.Object, new DocumentBuilder());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }

    //public class CoysAuctionsWebScraperTests
    //{
    //    [Test]
    //    [Category("Unit")]
    //    public void Import()
    //    {
    //        var sales = GetSales();
    //        Assert.AreEqual(75, sales.Count());
    //    }

    //    [Test]
    //    [Category("Unit")]
    //    public void PriceIsFormattedCorrectly1()
    //    {
    //        var sales = GetSales();
    //        Assert.AreEqual("€5,317", sales[5].Price);
    //    }

    //    [Test]
    //    [Category("Unit")]
    //    public void PriceIsFormattedCorrectly2()
    //    {
    //        var sales = GetSales();
    //        Assert.AreEqual("€354", sales[1].Price);
    //    }

    //    [Test]
    //    [Category("Unit")]
    //    public void DescriptionIsFormattedCorrectly1()
    //    {
    //        var sales = GetSales();
    //        Assert.AreEqual("1972 Cadillac Fleetwood", sales[5].Description);
    //    }

    //    [Test]
    //    [Category("Unit")]
    //    public void DescriptionIsFormattedCorrectly2()
    //    {
    //        var sales = GetSales();
    //        Assert.AreEqual("Jawa 350", sales[1].Description);
    //    }

    //    private static CoysAuction[] GetAuctions(string htmlFile = "/Html/SalesList.txt")
    //    {
    //        var mockHtmlLoader = new Mock<IHttpLoader>();

    //        mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
    //            .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

    //        var sut = new CoysAuctionsWebScraper(mockHtmlLoader.Object, new DocumentBuilder());
    //        var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
    //        return sales;
    //    }
    //}
}
