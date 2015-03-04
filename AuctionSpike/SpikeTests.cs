using Auctions.Export;
using Auctions.Export.Data;
using Auctions.Import.Barons;
using Auctions.Import.Barons.Model;
using Auctions.DomainModel;
using NUnit.Framework;

namespace AuctionSpike
{
    public class DataBaseTest
    {
        [Test]
        [Ignore]
        public void FullTest()
        {
            var auctionSalesScraper = new AuctionSalesDataImporter<BaronsSale>(new BaronsSalesWebDataImporter(),
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
