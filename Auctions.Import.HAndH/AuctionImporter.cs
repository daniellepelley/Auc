using System.Collections.Generic;
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

        private bool IsLastPage(AuctionSale[] auctionSales, int page)
        {
            return !auctionSales.Any() ||
                   auctionSales.Length < 12 ||
                   page > 1000;
        }

        public IEnumerable<AuctionSale> GetPages(string baseUrl)
        {
            var page = 1;
            do
            {
                var url = string.Format(baseUrl, page);
                var sales = _salesImporter.Import(url);

                foreach (var sale in sales)
                {
                    yield return sale;
                }

                if (IsLastPage(sales, page))
                {
                    break;
                }

                page++;
            }
            while (true);
        }

        public Auction Import(string baseUrl)
        {
            var auction = new Auction();
            auction.Sales.AddRange(GetPages(baseUrl));
            return auction;
        }
    }
}