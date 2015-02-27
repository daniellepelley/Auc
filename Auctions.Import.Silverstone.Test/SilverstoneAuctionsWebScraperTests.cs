using System;
using System.IO;
using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Import.Silverstone.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Silverstone.Test
{
    public class SilverstoneAuctionsWebScraperTests
    {
        [Test]
        [Category("Unit")]
        public void Import()
        {
            var sales = GetAuctions();
            Assert.AreEqual(25, sales.Count());
        }

        [Test]
        [Category("Unit")]
        public void NameIsFormattedCorrectly1()
        {
            var sales = GetAuctions();
            Assert.AreEqual("Race Retro Classic Car Sale", sales[0].Name);
        }

        [Test]
        [Category("Unit")]
        public void NameIsFormattedCorrectly2()
        {
            var sales = GetAuctions();
            Assert.AreEqual("NEC Classic Motor Show Sale", sales[1].Name);
        }

        [Test]
        [Category("Unit")]
        public void DateIsFormattedCorrectly1()
        {
            var sales = GetAuctions();
            Assert.AreEqual(new DateTime(2015, 2, 22), sales[0].Date);
        }

        [Test]
        [Category("Unit")]
        public void DateIsFormattedCorrectly2()
        {
            var sales = GetAuctions();
            Assert.AreEqual(new DateTime(2014, 11, 16), sales[1].Date);
        }

        private static SilverstoneAuction[] GetAuctions(string htmlFile = "/Html/AuctionsList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new SilverstoneAuctionsWebScraper(mockHtmlLoader.Object, new DocumentBuilder());
            var sales = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return sales;
        }
    }
}