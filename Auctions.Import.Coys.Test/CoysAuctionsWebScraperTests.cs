using System.IO;
using System.Linq;
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

//The Brundza Collection, Maastricht	Sat 10th January 2015 at 2:00pmView Results »
//Autosport, NEC	Sat 10th January 2015 at 1:00pmView Results »

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
            Assert.AreEqual("Sat 10th January 2015 at 2:00pm", sales[0].Date);
        }

        [Test]
        [Category("Unit")]
        public void DateIsFormattedCorrectly2()
        {
            var sales = GetAuctions();
            Assert.AreEqual("Sat 10th January 2015 at 1:00pm", sales[1].Date);
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