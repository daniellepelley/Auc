using System;
using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public abstract class HtmlWebDataImporter<T> : IWebDataImporter<T>, IDisposable
    {
        private readonly IDocumentBuilder _documentBuilder;
        private readonly IHttpLoader _httpLoader;
        private readonly IHtmlDocumentDataExtracter<T> _htmlDocumentDataExtracter;

        protected HtmlWebDataImporter(IHttpLoader httpLoader, IDocumentBuilder documentBuilder, IHtmlDocumentDataExtracter<T> htmlDocumentDataExtracter)
        {
            _htmlDocumentDataExtracter = htmlDocumentDataExtracter;
            _httpLoader = httpLoader;
            _documentBuilder = documentBuilder;
        }

        public async Task<T[]> Import(string url)
        {
            var html = await _httpLoader.Load(url);

            if (!string.IsNullOrEmpty(html))
            {
                var document = _documentBuilder.Build(html);
                return _htmlDocumentDataExtracter.GetData(document);                
            }

            return new T[0];
        }

        public void Dispose()
        {
            _httpLoader.Dispose();
        }
    }
}