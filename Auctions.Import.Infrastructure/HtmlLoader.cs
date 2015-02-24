using System;
using System.Net;

namespace Auctions.Import.Infrastructure
{
    public class HtmlLoader : IHtmlLoader
    {
        private bool _disposed;

        private readonly WebClient _webClient;

        public HtmlLoader()
        {
            _webClient = new WebClient();
        }

        public string Load(string url)
        {
            return _webClient.DownloadString(url);
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
