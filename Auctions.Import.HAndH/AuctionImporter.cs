using System.Linq;
using Auctions.Model;

namespace Auctions.Import.HAndH
{
    public class AuctionImporter : IAuctionImporter
    {
        private readonly ISalesImporter _salesImporter;

        public AuctionImporter(ISalesImporter salesImporter)
        {
            _salesImporter = salesImporter;
        }

        public Auction Import(string baseUrl)
        {
            var auction = new Auction();

            var page = 1;

            do
            {
                var url = string.Format(baseUrl, page);

                var sales = _salesImporter.Import(url);

                auction.Sales.AddRange(sales);

                if (!sales.Any() ||
                   sales.Length < 12)
                {
                    break;
                }

                page++;
            }
            while (true);

            return auction;
        }
    }
}