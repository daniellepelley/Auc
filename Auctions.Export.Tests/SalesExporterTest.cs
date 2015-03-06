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
        [Test]
        public void ExportingASingleSale()
        {
            var entities = new FakeAuctionEntitiesBuilder().Build();

            var updater = new Updater<Sale>(entities);

            var sut = new SalesExporter(entities, updater);

            sut.Export(new[]
            {
                new AuctionSale
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
                }
            });

            Assert.AreEqual(120, entities.Sales.First().Price);
            Assert.AreEqual("7", entities.Models.First().Name);
            Assert.AreEqual("Austin", entities.Makes.First().Name);
            Assert.AreEqual("Best Auction", entities.Auctions.First().Name);
        }

        [Test]
        public void When2IdenticalSalesAreUpdatedOnlyOneRecordIsCreated()
        {
            var entities = new FakeAuctionEntitiesBuilder().Build();

            var updater = new Updater<Sale>(entities);

            var sut = new SalesExporter(entities, updater);

            sut.Export(new[]
            {
                new AuctionSale
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
                },
                new AuctionSale
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
                }
            });

            Assert.AreEqual(1, entities.Models.Count());
            Assert.AreEqual(1, entities.Makes.Count());
            Assert.AreEqual(1, entities.Sales.Count());
            Assert.AreEqual("Best Auction", entities.Auctions.First().Name);
        }
    }
}