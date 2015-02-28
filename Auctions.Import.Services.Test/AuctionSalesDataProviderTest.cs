using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Services.Test
{
    public class AuctionSalesDataProviderTest
    {
        [Test]
        public void ImportServiceImportsAllAuctionSales()
        {
            var mockWebScraper = new Mock<IWebScraper<AuctionSale>>();
            mockWebScraper.Setup(x => x.Import("foo"))
                .ReturnsAsync(new[] { new AuctionSale() });

            mockWebScraper.Setup(x => x.Import("bar"))
                .ReturnsAsync(new[] { new AuctionSale(), new AuctionSale() });

            var mockUrlProvider = new Mock<IAuctionListingProvider>();
            mockUrlProvider.Setup(x => x.GetAuctionListings())
                .ReturnsAsync(new[]
                {
                    new AuctionListing
                    {
                        Url = "foo"
                    },
                    new AuctionListing
                    {
                        Url = "bar"
                    }
                });

            var sut = new AuctionSalesDataProvider(mockWebScraper.Object, mockUrlProvider.Object);
            var sales = sut.GetData().Result;
            Assert.AreEqual(3, sales.Count());
        }
    }
}