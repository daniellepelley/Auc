using System.Linq;
using NUnit.Framework;

namespace Auctions.Import.Barons.Test
{
    public class IntegrationTests
    {
        [Test]
        [Ignore]
        [Category("Integration")]
        public void PageImporterImport()
        {
            var sut = new BaronsSalesWebScraper();
            var sales = sut.Import("http://www.barons-auctions.com/fullresults.php");

            Assert.IsTrue(sales.Count() > 1000);
        }
    }
}