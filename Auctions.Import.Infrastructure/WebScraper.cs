using System;
using HtmlAgilityPack;

namespace Auctions.Import.Infrastructure
{
    public abstract class WebScraper<T> : IWebScraper<T>, IDisposable
    {
        private readonly IDocumentBuilder _documentBuilder;
        private readonly IHtmlLoader _htmlLoader;

        protected WebScraper(IHtmlLoader htmlLoader, IDocumentBuilder documentBuilder)
        {
            _htmlLoader = htmlLoader;
            _documentBuilder = documentBuilder;
        }

        public T[] Import(string url)
        {
            var html = _htmlLoader.Load(url);
            var document = _documentBuilder.Build(html);
            return GetData(document);
        }

        protected abstract T[] GetData(HtmlDocument htmlDocument);

        public void Dispose()
        {
            _htmlLoader.Dispose();
        }
    }
}