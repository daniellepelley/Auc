using System;
using System.IO;
using System.Linq;
using Auctions.Import.Coys.Model;
using Auctions.Import.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Coys.Test
{
    public class CoysAuctionsWebScraperTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var sales = GetAuctions();
            Assert.AreEqual(33, sales.Count());
        }

        [Test]
        [Category("Unit")]
        public void NameIsFormattedCorrectly1()
        {
            var sales = GetAuctions();
            Assert.AreEqual("The Brundza Collection, Maastricht", sales[0].Name);
        }

        [Test]
        [Category("Unit")]
        public void NameIsFormattedCorrectly2()
        {
            var sales = GetAuctions();
            Assert.AreEqual("Autosport, NEC", sales[1].Name);
        }

        [Test]
        [Category("Unit")]
        public void DateIsFormattedCorrectly1()
        {
            var sales = GetAuctions();
            Assert.AreEqual(new DateTime(2015, 1, 10), sales[0].Date);
        }

        [Test]
        [Category("Unit")]
        public void DateIsFormattedCorrectly2()
        {
            var sales = GetAuctions();
            Assert.AreEqual(new DateTime(2014, 5, 9), sales[7].Date);
        }

        private static CoysAuction[] GetAuctions(string htmlFile = "/Html/AuctionsList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new CoysAuctionsWebScraper(mockHtmlLoader.Object, new DocumentBuilder());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}