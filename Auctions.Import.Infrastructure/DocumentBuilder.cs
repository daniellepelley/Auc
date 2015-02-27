using HtmlAgilityPack;

namespace Auctions.Import.Infrastructure
{
    public class DocumentBuilder : IDocumentBuilder
    {
        public HtmlDocument Build(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }
    }
}