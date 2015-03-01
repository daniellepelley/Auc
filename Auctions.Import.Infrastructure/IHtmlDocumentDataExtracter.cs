using HtmlAgilityPack;

namespace Auctions.Import.Infrastructure
{
    public interface IHtmlDocumentDataExtracter<out T>
    {
        T[] GetData(HtmlDocument htmlDocument);
    }
}