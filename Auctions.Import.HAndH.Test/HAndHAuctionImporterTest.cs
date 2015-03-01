using System.IO;
using Auctions.Import.HAndH.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class HAndHAuctionImporterTest
    {
        private Mock<IHttpLoader> _mockHtmlLoader;

        [Test]
        [Category("Unit")]
        public void ImportAuction()
        {
            IWebScraper<AuctionSale> auctionImporter = CreateSut();

            var sales =
                auctionImporter.Import(
                    "http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p={0}").Result;

            Assert.AreEqual(15, sales.Length);
        }

        [Test]
        [Category("Unit")]
        public void ImportAuctionVerifyCalls()
        {
            IWebScraper<AuctionSale> auctionImporter = CreateSut();

            auctionImporter.Import(
                "http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p={0}");

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

        private IWebScraper<AuctionSale> CreateSut()
        {
            _mockHtmlLoader = new Mock<IHttpLoader>();

            _mockHtmlLoader.Setup(
                x =>
                    x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=1"))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/PageHtml.txt"));

            _mockHtmlLoader.Setup(
                x =>
                    x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=2"))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/PageHtml2.txt"));

            _mockHtmlLoader.Setup(
                x =>
                    x.Load("http://www.classic-auctions.com/Auctions/24-04-2014-ImperialWarMuseumDuxford-1365.aspx?p=3"))
                .ReturnsAsync(File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/PageHtmlEmpty.txt"));

            var webScraper = new HAndHSalesWebScraper(_mockHtmlLoader.Object, new DocumentBuilder());

            var salesImporter = new AuctionSalesScraper<HAndHSale>(webScraper, new HandHSalesMapper());

            var auctionImporter = new HAndHAuctionImporter(salesImporter);

            return auctionImporter;
        }
    }
}