using System.Linq;
using Auction.Reporting.DomainModel;
using Auctions.Data.Ef;
using Auctions.Export.Tests.Builders;
using Auctions.DomainModel;
using NUnit.Framework;

namespace Auctions.Export.Tests
{
    public class SalesExporterTest
    {
        private SalesExporter _sut;
        private AuctionEntities _auctionEntities;

        [SetUp]
        public void SetUp()
        {
            _auctionEntities = new FakeAuctionEntitiesBuilder().Build();
            var updater = new Updater<Sale>(_auctionEntities);
            _sut = new SalesExporter(_auctionEntities, updater);
        }

        [Test]
        public void ExportingASingleSale()
        {
            _sut.Export(new[]
            {
                CreateAuctionSale()
            });

            Assert.AreEqual(120, _auctionEntities.Sales.First().Price);
            Assert.AreEqual("7", _auctionEntities.Models.First().Name);
            Assert.AreEqual("Austin", _auctionEntities.Makes.First().Name);
            Assert.AreEqual("Best Auction", _auctionEntities.Auctions.First().Name);
        }

        [Test]
        public void When2IdenticalSalesAreUpdatedOnlyOneRecordIsCreated()
        {
            _sut.Export(new[]
            {
                CreateAuctionSale(),
                CreateAuctionSale()
            });

            Assert.AreEqual(1, _auctionEntities.Models.Count());
            Assert.AreEqual(1, _auctionEntities.Makes.Count());
            Assert.AreEqual(1, _auctionEntities.Sales.Count());
            Assert.AreEqual("Best Auction", _auctionEntities.Auctions.First().Name);
        }

        [Test]
        public void WhenASaleIsIdenticalToAPreviousSaleNoNewRecordIsCreated()
        {
            _sut.Export(new[]
            {
                CreateAuctionSale()
            });

            _sut.Export(new[]
            {
                CreateAuctionSale()
            });

            Assert.AreEqual(1, _auctionEntities.Models.Count());
            Assert.AreEqual(1, _auctionEntities.Makes.Count());
            Assert.AreEqual(1, _auctionEntities.Sales.Count());
            Assert.AreEqual("Best Auction", _auctionEntities.Auctions.First().Name);
        }

        private static AuctionSale CreateAuctionSale()
        {
            return new AuctionSale
            {
                Make = "Austin",
                Model = "7",
                Price = 120,
                Sold = true,
                Year = 1937,
                AuctionListing = new AuctionListing
                {
                    Name = "Best Auction"
                }
            };
        }
    }
}