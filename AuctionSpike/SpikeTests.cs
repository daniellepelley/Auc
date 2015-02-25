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
            var importer = new SalesImporter<BaronsSale>(new BaronsSalesWebScraper(),
                new BaronsSalesMapper());

            //var sales = new AuctionSale[0];
            var sales = importer.Import("http://www.barons-auctions.com/fullresults.php");


            var auctionEntities = new AuctionEntities();

            auctionEntities.Configuration.AutoDetectChangesEnabled = false;
            auctionEntities.Configuration.ValidateOnSaveEnabled = false;

            auctionEntities.Database.ExecuteSqlCommand("TRUNCATE TABLE SALES");

            var exporter = new SalesExporter(auctionEntities, new Updater<Auctions.Export.Sale>(auctionEntities));

            exporter.Export(sales);


        }
    }

}
