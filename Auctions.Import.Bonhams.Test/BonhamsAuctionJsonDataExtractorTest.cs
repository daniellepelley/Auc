using System;
using System.IO;
using System.Linq;
using Auctions.Import.Bonhams.Model;
using NUnit.Framework;

namespace Auctions.Import.Bonhams.Test
{
    public class BonhamsAuctionJsonDataExtractorTest
    {
        [Test]
        public void AreMany()
        {
            var auctions = GetBonhamsAuctions();
            Assert.IsTrue(auctions.Any());
        }

        [Test]
        public void IdIsFormattedCorrectly1()
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual("22528", auctions.First().Id);
        }

        [Test]
        public void IdIsFormattedCorrectly2()
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual("22205", auctions[1].Id);
        }

        [Test]
        public void NameIsFormattedCorrectly1()
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual("Les Grandes Marques du Monde au Grand Palais", auctions[0].Name);
        }

        [Test]
        public void NameIsFormattedCorrectly2()
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual("The Scottsdale Auction", auctions[1].Name);
        }

        [Test]
        public void DateIsFormattedCorrectly1()
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual(new DateTime(2015, 2, 5), auctions[0].Date);
        }

        [Test]
        public void DateIsFormattedCorrectly2()
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual(new DateTime(2015, 1, 15), auctions[1].Date);
        }


        private static BonhamsAuction[] GetBonhamsAuctions()
        {
            var json = File.ReadAllText(Directory.GetCurrentDirectory() + "/Json/AuctionsList.json");

            var sut = new BonhamsAuctionJsonDataExtractor();

            var list = sut.Extract(json);
            return list;
        }
    }
}