using System.Collections.Generic;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;

namespace Auctions.Model
{
    public class AuctionSalesImporter : ISalesImporter
    {
        private readonly IWebScraper<AuctionSale> _webScraper;
        private readonly IUrlProvider _urlProvider;

        public AuctionSalesImporter(IWebScraper<AuctionSale> webScraper, IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
            _webScraper = webScraper;
        }

        public async Task<AuctionSale[]> Import()
        {
            var list = new List<AuctionSale>();

            var urls = await _urlProvider.GetUrls();

            foreach (var url in urls)
            {
                list.AddRange(await _webScraper.Import(url));
            }

            return list.ToArray();
        }
    }
}