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
            var auctions = GetAuctions();
            Assert.AreEqual(33, auctions.Count());
        }

        [Test]
        [Category("Unit")]
        public void NameIsFormattedCorrectly1()
        {
            var auctions = GetAuctions();
            Assert.AreEqual("The Brundza Collection, Maastricht", auctions[0].Name);
        }

        [Test]
        [Category("Unit")]
        public void NameIsFormattedCorrectly2()
        {
            var auctions = GetAuctions();
            Assert.AreEqual("Autosport, NEC", auctions[1].Name);
        }

        [Test]
        [Category("Unit")]
        public void DateIsFormattedCorrectly1()
        {
            var auctions = GetAuctions();
            Assert.AreEqual(new DateTime(2015, 1, 10), auctions[0].Date);
        }

        [Test]
        [Category("Unit")]
        public void DateIsFormattedCorrectly2()
        {
            var auctions = GetAuctions();
            Assert.AreEqual(new DateTime(2014, 5, 9), auctions[7].Date);
        }

        [Test]
        [Category("Unit")]
        public void IdIsFormattedCorrectly1()
        {
            var auctions = GetAuctions();
            Assert.AreEqual("51", auctions[0].Id);
        }

        [Test]
        [Category("Unit")]
        public void IdIsFormattedCorrectly2()
        {
            var auctions = GetAuctions();
            Assert.AreEqual("47", auctions[4].Id);
        }

        private static CoysAuction[] GetAuctions(string htmlFile = "/Html/AuctionsList.txt")
        {
            var mockHtmlLoader = new Mock<IHttpLoader>();

            mockHtmlLoader.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + htmlFile));

            var sut = new CoysAuctionsWebScraper(mockHtmlLoader.Object, new DocumentBuilder());
            var auctions = sut.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx").Result;
            return auctions;
        }
    }
}