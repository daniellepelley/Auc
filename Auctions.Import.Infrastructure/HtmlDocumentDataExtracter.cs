using System;
using System.Linq;
using HtmlAgilityPack;

namespace Auctions.Import.Infrastructure
{
    public class HtmlDocumentDataExtracter<T> : IHtmlDocumentDataExtracter<T>
    {
        private readonly string _xpath;
        private readonly int _setNumber;
        private readonly Func<HtmlNode[], T> _createFunc;
        private readonly Func<T, bool> _filterFunc;

        public HtmlDocumentDataExtracter(string xpath, int setNumber, Func<HtmlNode[], T> createFunc, Func<T, bool> filterFunc = null)
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
}