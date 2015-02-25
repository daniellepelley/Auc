using System.Linq;
using Effort;
using NUnit.Framework;

namespace Auctions.Export.Tests
{
    public class FakeDatabaseTest
    {
        [Test]
        public void Connect()
        {
            var entities = new AuctionEntities(DbConnectionFactory.CreatePersistent("name=AuctionEntities"));
            entities.Makes.Add(new Make {Name = "Ford"});
            entities.SaveChanges();
            Assert.AreEqual(1, entities.Makes.Count());
        }
    }
}
