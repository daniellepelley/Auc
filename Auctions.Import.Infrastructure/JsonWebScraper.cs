using System;
using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public class JsonWebScraper<T> : IWebScraper<T>, IDisposable
    {
        private readonly IHttpLoader _httpLoader;
        private readonly IJsonDataExtractor<T> _dataExtractor;
        private IMonitor _monitor;

        public JsonWebScraper(IHttpLoader httpLoader, IJsonDataExtractor<T> dataExtractor, IMonitor monitor)
        {
            _monitor = monitor;
            _dataExtractor = dataExtractor;
            _httpLoader = httpLoader;
        }

        public async Task<T[]> Import(string url)
        {
            _monitor.Update(url);
            var json = await _httpLoader.Load(url);
            return _dataExtractor.Extract(json);
        }

        public void Dispose()
        {
            _httpLoader.Dispose();
        }
    }
}