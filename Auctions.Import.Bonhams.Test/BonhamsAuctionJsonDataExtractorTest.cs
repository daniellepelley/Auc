using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Auctions.Import.Bonhams.Test
{
    public class BonhamsAuctionJsonDataExtractorTest
    {
        [Test]
        public void Extract()
        {
            var json = File.ReadAllText(Directory.GetCurrentDirectory() + "/Json/AuctionsList.json");

            var sut = new BonhamsAuctionJsonDataExtractor();

            var list = sut.Extract(json);

            Assert.IsTrue(list.Any());
            Assert.AreEqual("22528", list.First().Id);
            Assert.AreEqual("Les Grandes Marques du Monde au Grand Palais", list.First().Name);
        }
    }
}