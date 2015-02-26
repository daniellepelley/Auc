using System.IO;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class AuctionImporterTest
    {
        [Test]
        [Category("Unit")]
        public void ImportAuction()
        {
            var auctionImporter = CreateSut();

            var auction = auctionImporter.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p={0}");

            Assert.AreEqual(15, auction.Sales.Count);
        }

        [Test]
        [Category("Unit")]
        public void ImportAuctionVerifyCalls()
        {
            var auctionImporter = CreateSut();

            var auction = auctionImporter.Import("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p={0}");

            _mockHtmlLoader.Verify(
                x =>
                    x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=1"),
                    Times.Once);

            _mockHtmlLoader.Verify(
                x =>
                    x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=2"),
                    Times.Once);

            _mockHtmlLoader.Verify(
                x =>
                    x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=3"),
                    Times.Never);
        }

        private Mock<IHttpLoader> _mockHtmlLoader;

        private IAuctionImporter CreateSut()
        {
            _mockHtmlLoader = new Mock<IHttpLoader>();

            _mockHtmlLoader.Setup(x => x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=1"))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/PageHtml.txt"));

            _mockHtmlLoader.Setup(x => x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=2"))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/PageHtml2.txt"));

            _mockHtmlLoader.Setup(x => x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=3"))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/PageHtmlEmpty.txt"));

            var webScraper = new HAndHSalesWebScraper(_mockHtmlLoader.Object, new DocumentBuilder());

            var salesImporter = new AuctionSalesScraper<HAndHSale>(webScraper, new HandHSalesMapper());

            var auctionImporter = new AuctionImporter(salesImporter);

            return auctionImporter;
        }
    }
}