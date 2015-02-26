using Auctions.Export;
using Auctions.Import.Barons;
using Auctions.Model;
using NUnit.Framework;

namespace AuctionSpike
{
    public class DataBaseTest
    {
        [Test]
        [Ignore]
        public void FullTest()
        {
            var auctionSalesScraper = new AuctionSalesScraper<BaronsSale>(new BaronsSalesWebScraper(),
                new BaronsSalesMapper());

            var sales = auctionSalesScraper.Import("http://www.barons-auctions.com/fullresults.php").Result;

            var auctionEntities = new AuctionEntities();

            auctionEntities.Configuration.AutoDetectChangesEnabled = false;
            auctionEntities.Configuration.ValidateOnSaveEnabled = false;

            auctionEntities.Database.ExecuteSqlCommand("TRUNCATE TABLE SALES");

            var exporter = new SalesExporter(auctionEntities, new Updater<Sale>(auctionEntities));

            exporter.Export(sales);


        }
    }

}
