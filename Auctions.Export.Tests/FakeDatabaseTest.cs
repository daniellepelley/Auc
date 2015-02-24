using System.Linq;
using NUnit.Framework;

namespace Auctions.Export.Tests
{
    public class FakeDatabaseTest
    {
        [Test]
        public void Connect()
        {
            var entities = new AuctionEntities(Effort.DbConnectionFactory.CreatePersistent("name=AuctionEntities"));
            entities.Makes.Add(new Make {Name = "Ford"});
            entities.SaveChanges();
            Assert.AreEqual(1, entities.Makes.Count());
        }
    }

    public class SalesExporterTest
    {
        [Test]
        public void Connect()
        {
            var entities = new AuctionEntities(Effort.DbConnectionFactory.CreateTransient());

            var sut = new SalesExporter(entities);

            sut.Export(new[]
            {
                new Auctions.Model.Sale
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
    }
}
