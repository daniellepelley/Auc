using System.Linq;
using Auctions.Import.Infrastructure;

namespace Auctions.Model
{
    public class SalesImporter<T> : ISalesImporter
    {
        private readonly IWebScraper<T> _webScraper;
        private readonly ISaleMapper<T> _saleMapper;

        public SalesImporter(IWebScraper<T> webScraper, ISaleMapper<T> saleMapper)
        {
            _saleMapper = saleMapper;
            _webScraper = webScraper;
        }

        public AuctionSale[] Import(string url)
        {
            return _webScraper
                .Import(url)
                .Select(_saleMapper.Map)
                .ToArray();
        }
    }
}