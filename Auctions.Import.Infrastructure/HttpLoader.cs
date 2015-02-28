using System;
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
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            try
            {
                return await _webClient.DownloadStringTaskAsync(url);
            }
            catch (Exception)
            {
                return null;
            }
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
