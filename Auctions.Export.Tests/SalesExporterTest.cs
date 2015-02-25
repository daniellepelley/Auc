using System.Linq;
using Auctions.Model;
using Effort;
using NUnit.Framework;

namespace Auctions.Export.Tests
{
    public class SalesExporterTest
    {
        [Test]
        public void ExportingASingleSale()
        {
            var entities = new AuctionEntities(DbConnectionFactory.CreateTransient());

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

            Assert.AreEqual(120, entities.Sales.FirstOrDefault().Price);
            Assert.AreEqual("7", entities.Models.FirstOrDefault().Name);
            Assert.AreEqual("Austin", entities.Makes.FirstOrDefault().Name);
        }

        [Test]
        public void When2IdenticalSalesAreUpdatedOnlyOneRecordIsCreated()
        {
            var entities = new AuctionEntities(DbConnectionFactory.CreateTransient());

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