using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;

namespace Auctions.Model
{
    public class AuctionImporter : IWebScraper<AuctionSale>
    {
        private readonly IWebScraper<AuctionSale>  _salesScraper;
        private readonly Func<AuctionSale[], bool> _isLastPageFunc;

        public AuctionImporter(IWebScraper<AuctionSale> salesScraper, Func<AuctionSale[], bool> isLastPageFunc)
        {
            _isLastPageFunc = isLastPageFunc;
            _salesScraper = salesScraper;
        }

        public async Task<AuctionSale[]> Import(string url)
        {
            var urlProvider = new IncrementalUrlProvider(url);

            var output = new List<AuctionSale>();

            foreach (var u in urlProvider.GetUrls())
            {
                var sales = await _salesScraper.Import(u);

                output.AddRange(sales);

                if (_isLastPageFunc(sales))
                {
                    break;
                }
            }
            return output.ToArray();
        }
    }
}