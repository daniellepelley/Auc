using HtmlAgilityPack;

namespace Auctions.Import.Infrastructure
{
    public interface IDocumentBuilder
    {
        HtmlDocument Build(string html);
    }
}
