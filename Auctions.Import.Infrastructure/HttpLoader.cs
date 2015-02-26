using System.Net;
using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public class HttpLoader : IHttpLoader
    {
        private bool _disposed;

        private readonly WebClient _webClient;

        public HttpLoader()
        {
            _webClient = new WebClient();
        }

        public async Task<string> Load(string url)
        {
            return await _webClient.DownloadStringTaskAsync(url);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _webClient.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
