using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.HAndH
{
    public class HAndHAuctionImporter : AuctionImporter
    {
        public HAndHAuctionImporter(IWebDataImporter<AuctionSale> salesDataImporter)
            : base(salesDataImporter, IsLastPage)
        { }

        private static bool IsLastPage(AuctionSale[] auctionSales)
        {
            return !auctionSales.Any() ||
                   auctionSales.Length < 12;
        }
    }
}