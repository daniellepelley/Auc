using System.Collections.Generic;
using System.Linq;
using Auctions.Import.HAndH.Model;
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

        private bool IsLastPage(AuctionSale[] auctionSales)
        {
            return !auctionSales.Any() ||
                   auctionSales.Length < 12;
        }

        private IEnumerable<AuctionSale> GetPages(string baseUrl)
        {
            var urlProvider = new HAndHUrlProvider(baseUrl);

            foreach (var url in urlProvider.GetUrls())
            {
                var sales = _salesScraper.Import(url).Result;

                foreach (var sale in sales)
                {
                    yield return sale;
                }

                if (IsLastPage(sales))
                {
                    break;
                }
            }
        }

        public HAndHAuction Import(string baseUrl)
        {
            var auction = new HAndHAuction();
            auction.Sales.AddRange(GetPages(baseUrl));
            return auction;
        }
    }

    public class HAndHUrlProvider : IUrlProvider
    {
        private readonly string _baseUrl;

        public HAndHUrlProvider(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public IEnumerable<string> GetUrls()
        {
            var page = 1;

            do
            {
                yield return string.Format(_baseUrl, page);
                page++;
            }
            while (page < 10000);
        }
    }
}