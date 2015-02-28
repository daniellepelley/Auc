using System;
using System.IO;
using System.Linq;
using Auctions.Import.Bonhams.Model;
using Auctions.Model;
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

        [TestCase(0, "https://www.bonhams.com/api/v1/lots/22528/?category=results&length=540&minimal=false&page=1")]
        [TestCase(1, "https://www.bonhams.com/api/v1/lots/22205/?category=results&length=540&minimal=false&page=1")]        
        [Category("Unit")]
        public void UrlIsFormattedCorrectly(int index, string expected)
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual(expected, auctions[index].Url);
        }

        [TestCase(0, "Les Grandes Marques du Monde au Grand Palais")]
        [TestCase(1, "The Scottsdale Auction")]
        [Category("Unit")]
        public void NameIsFormattedCorrectly1(int index, string expected)
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual(expected, auctions[index].Name);
        }

        [TestCase(0, "05/02/2015")]
        [TestCase(1, "15/01/2015")]
        [Category("Unit")]
        public void DateIsFormattedCorrectly(int index, string expected)
        {
            var auctions = GetBonhamsAuctions();
            Assert.AreEqual(expected, auctions[index].Date.Value.ToString("dd/MM/yyyy"));
        }

        private static AuctionListing[] GetBonhamsAuctions()
        {
            var json = File.ReadAllText(Directory.GetCurrentDirectory() + "/Json/AuctionsList.json");

            var sut = new BonhamsAuctionJsonDataExtractor();

            var list = sut.Extract(json);
            return list;
        }
    }
}