//using System.Linq;
//using Auctions.Import.Infrastructure;
//using Auctions.Model;

//namespace Auctions.Import.Silverstone
//{
//    public class SilverstoneAuctionImporter : AuctionImporter
//    {
//        public SilverstoneAuctionImporter(IWebDataImporter<AuctionSale> salesScraper)
//            : base(salesScraper, IsLastPage)
//        { }

//        private static bool IsLastPage(AuctionSale[] auctionSales)
//        {
//            return !auctionSales.Any() ||
//                   auctionSales.Length < 10;
//        }
//    }
//}