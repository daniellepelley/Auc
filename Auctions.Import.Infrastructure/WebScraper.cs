using System;

namespace Auctions.Import.Infrastructure
{
    public abstract class WebScraper<T> : IWebScraper<T>, IDisposable
    {
        private readonly IDocumentBuilder _documentBuilder;
        private readonly IHtmlLoader _htmlLoader;
        private readonly IHtmlDocumentDataExtracter<T> _htmlDocumentDataExtracter;

        protected WebScraper(IHtmlLoader htmlLoader, IDocumentBuilder documentBuilder, IHtmlDocumentDataExtracter<T> htmlDocumentDataExtracter)
        {
            _htmlDocumentDataExtracter = htmlDocumentDataExtracter;
            _htmlLoader = htmlLoader;
            _documentBuilder = documentBuilder;
        }

        public T[] Import(string url)
        {
            var html = _htmlLoader.Load(url);
            var document = _documentBuilder.Build(html);
            return _htmlDocumentDataExtracter.GetData(document);
        }

        public void Dispose()
        {
            _htmlLoader.Dispose();
        }
    }
}