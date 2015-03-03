using System.Linq;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;

namespace Auctions.Model
{
    public class AuctionSalesScraper<T> : IWebScraper<AuctionSale>
    {
        private readonly IWebScraper<T> _webScraper;
        private readonly ISaleMapper<T> _saleMapper;

        public AuctionSalesScraper(IWebScraper<T> webScraper, ISaleMapper<T> saleMapper)
        {
            _saleMapper = saleMapper;
            _webScraper = webScraper;
        }

        public async Task<AuctionSale[]> Import(string url)
        {
            var result = await _webScraper.Import(url);

            return
                result.Select(_saleMapper.Map)
                .ToArray();
        }
    }
}