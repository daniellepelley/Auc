using System;
using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public abstract class HtmlWebScraper<T> : IWebScraper<T>, IDisposable
    {
        private readonly IDocumentBuilder _documentBuilder;
        private readonly IHttpLoader _httpLoader;
        private readonly IHtmlDocumentDataExtracter<T> _htmlDocumentDataExtracter;

        protected HtmlWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder, IHtmlDocumentDataExtracter<T> htmlDocumentDataExtracter)
        {
            _htmlDocumentDataExtracter = htmlDocumentDataExtracter;
            _httpLoader = httpLoader;
            _documentBuilder = documentBuilder;
        }

        public async Task<T[]> Import(string url)
        {
            var html = await _httpLoader.Load(url);
            var document = _documentBuilder.Build(html);
            return _htmlDocumentDataExtracter.GetData(document);
        }

        public void Dispose()
        {
            _httpLoader.Dispose();
        }
    }
}