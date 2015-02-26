using System.Collections.Generic;
using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.HAndH
{
    public class AuctionImporter : IAuctionImporter
    {
        private readonly IWebScraper<AuctionSale>  _salesScraper;

        public AuctionImporter(IWebScraper<AuctionSale> salesScraper)
        {
            _salesScraper = salesScraper;
        }

        private bool IsLastPage(AuctionSale[] auctionSales, int page)
        {
            return !auctionSales.Any() ||
                   auctionSales.Length < 12 ||
                   page > 1000;
        }

        private IEnumerable<AuctionSale> GetPages(string baseUrl)
        {
            var page = 1;
            do
            {
                var url = string.Format(baseUrl, page);
                var sales = _salesScraper.Import(url).Result;

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

        public HAndHAuction Import(string baseUrl)
        {
            var auction = new HAndHAuction();
            auction.Sales.AddRange(GetPages(baseUrl));
            return auction;
        }
    }
}