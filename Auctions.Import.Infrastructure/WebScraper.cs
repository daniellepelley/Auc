using System;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace Auctions.Import.Infrastructure
{
    public interface IDataExtracter<T>
    {
        T[] GetData(HtmlDocument htmlDocument);
    }

    public class DataExtracter<T> : IDataExtracter<T>
    {
        private readonly string _xpath;
        private readonly int _setNumber;
        private readonly Func<HtmlNode[], T> _createFunc;
        private readonly Func<T, bool> _filterFunc;

        public DataExtracter(string xpath, int setNumber, Func<HtmlNode[], T> createFunc, Func<T, bool> filterFunc = null)
        {
            _filterFunc = filterFunc ?? (x => true);
            _createFunc = createFunc;
            _setNumber = setNumber;
            _xpath = xpath;
        }

        public T[] GetData(HtmlDocument htmlDocument)
        {
            var cells = htmlDocument.DocumentNode.SelectNodes(_xpath);

            if (cells == null)
            {
                return new T[0];
            }

            return cells
                .InSetsOf(_setNumber)
                .Where(tds => tds.Count() == _setNumber)
                .Select(_createFunc)
                .Where(_filterFunc)
                .ToArray();
        }
    }

    public abstract class WebScraper<T> : IWebScraper<T>, IDisposable
    {
        private readonly IDocumentBuilder _documentBuilder;
        private readonly IHtmlLoader _htmlLoader;
        private readonly IDataExtracter<T> _dataExtracter;

        protected WebScraper(IHtmlLoader htmlLoader, IDocumentBuilder documentBuilder, IDataExtracter<T> dataExtracter)
        {
            _dataExtracter = dataExtracter;
            _htmlLoader = htmlLoader;
            _documentBuilder = documentBuilder;
        }

        public T[] Import(string url)
        {
            var html = _htmlLoader.Load(url);
            var document = _documentBuilder.Build(html);
            return _dataExtracter.GetData(document);
        }

        public void Dispose()
        {
            _htmlLoader.Dispose();
        }
    }
}