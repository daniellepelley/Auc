using HtmlAgilityPack;

namespace Auctions.Import.Infrastructure
{
    public interface IHtmlDocumentDataExtracter<T>
    {
        T[] GetData(HtmlDocument htmlDocument);
    }
}