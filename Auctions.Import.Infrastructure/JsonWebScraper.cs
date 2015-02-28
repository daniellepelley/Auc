using System;
using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public class JsonWebScraper<T> : IWebScraper<T>, IDisposable
    {
        private readonly IHttpLoader _httpLoader;
        private readonly IJsonDataExtractor<T> _dataExtractor;

        public JsonWebScraper(IHttpLoader httpLoader, IJsonDataExtractor<T> dataExtractor)
        {
            _dataExtractor = dataExtractor;
            _httpLoader = httpLoader;
        }

        public async Task<T[]> Import(string url)
        {
            var json = await _httpLoader.Load(url);
            return _dataExtractor.Extract(json);
        }

        public void Dispose()
        {
            _httpLoader.Dispose();
        }
    }
}