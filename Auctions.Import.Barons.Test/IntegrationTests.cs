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
            var sut = new BaronsSalesWebDataImporter();
            var sales = sut.Import("http://www.barons-auctions.com/fullresults.php");

            Assert.IsTrue(sales.Result.Length > 1000);
        }
    }
}