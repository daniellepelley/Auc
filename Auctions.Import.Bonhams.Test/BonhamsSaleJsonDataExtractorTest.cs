//using System.IO;
//using System.Linq;
//using NUnit.Framework;

//namespace Auctions.Import.Bonhams.Test
//{
//    public class BonhamsSaleJsonDataExtractorTest
//    {
//        [Test]
//        public void Extract()
//        {
//            var json = File.ReadAllText(Directory.GetCurrentDirectory() + "/Json/AuctionSalesList.json");

//            var sut = new BonhamsSaleJsonDataExtractor();

//            var list = sut.Extract(json);

//            Assert.IsTrue(list.Any());
//            Assert.AreEqual("Assorted books and literature relating to the Bentley marque, ((Qty))", list.First().Description);
//            Assert.AreEqual("625", list.First().Price);
//            Assert.AreEqual("£", list.First().Currency);
//        }
//    }
//}