using System.Linq;

namespace Auctions.Import.Infrastructure
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

        public Sale[] Import(string url)
        {
            return _webScraper
                .Import(url)
                .Select(_saleMapper.Map)
                .ToArray();
        }
    }
}