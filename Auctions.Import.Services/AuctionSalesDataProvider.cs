using System.Collections.Generic;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.Services
{
    public class AuctionSalesDataProvider : IDataProvider<AuctionSale>
    {
        private readonly IUrlProvider _urlProvider;
        private readonly IWebScraper<AuctionSale> _webScraper;

        public AuctionSalesDataProvider(IWebScraper<AuctionSale> webScraper, IUrlProvider urlProvider)
        {
            _webScraper = webScraper;
            _urlProvider = urlProvider;
        }

        public async Task<AuctionSale[]> GetData()
        {
            var auctionSales = new List<AuctionSale>();

            var urls = await _urlProvider.GetUrls();

            foreach (var url in urls)
            {
                auctionSales.AddRange(await _webScraper.Import(url));
            }

            return auctionSales.ToArray();
        }
    }
}