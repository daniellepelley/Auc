using System.Net;

namespace Auctions.Import.Infrastructure
{
    public class HtmlLoader : IHtmlLoader
    {
        private readonly WebClient _webClient;

        public HtmlLoader()
        {
            _webClient = new WebClient();
        }

        public string Load(string url)
        {
            return _webClient.DownloadString(url);
        }

        //TODO: Implement IDisposible
        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
