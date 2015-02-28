using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctions.Import.HAndH.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.HAndH
{
    public class AuctionImporter : IWebScraper<AuctionSale>
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

        //public HAndHAuction Import2(string baseUrl)
        //{
        //    var auction = new HAndHAuction();
        //    auction.Sales.AddRange(GetPages(baseUrl));
        //    return auction;
        //}

        public async Task<AuctionSale[]> Import(string url)
        {
            var urlProvider = new HAndHUrlProvider(url);

            var output = new List<AuctionSale>();

            foreach (var u in urlProvider.GetUrls())
            {
                var sales = await _salesScraper.Import(u);

                output.AddRange(sales);

                if (IsLastPage(sales))
                {
                    break;
                }
            }
            return output.ToArray();
        }
    }
}