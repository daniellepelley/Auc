using System.Linq;
using Auctions.Export.Tests.Builders;
using Auctions.Model;
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
                    Year = 1937
                }
            });

            Assert.AreEqual(120, entities.Sales.First().Price);
            Assert.AreEqual("7", entities.Models.First().Name);
            Assert.AreEqual("Austin", entities.Makes.First().Name);
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
                    Year = 1937
                },
                new AuctionSale
                {
                    Make = "Austin",
                    Model = "7",
                    Price = 120,
                    Sold = true,
                    Year = 1937
                }
            });

            Assert.AreEqual(1, entities.Models.Count());
            Assert.AreEqual(1, entities.Makes.Count());
            Assert.AreEqual(1, entities.Sales.Count());
        }
    }
}