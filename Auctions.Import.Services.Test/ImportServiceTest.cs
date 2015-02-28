using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Model;
using Moq;
using NUnit.Framework;

namespace Auctions.Import.Services.Test
{
    public class ImportServiceTest
    {
        [Test]
        public void ImportServiceImportsAllAuctionSales()
        {
            var mockDataProvider = new Mock<IDataProvider<AuctionSale>>();

            mockDataProvider.Setup(x => x.GetData())
                .ReturnsAsync(new[] {new AuctionSale()});

            var sut = new AuctionSalesImportService(new[] { mockDataProvider.Object });
            var sales = sut.Import().Result;
            Assert.AreEqual(1, sales.Count());
        } 
    }
}
