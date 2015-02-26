using System.Web;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.Coys
{
    public class CoysSalesWebScraper : HtmlWebScraper<CoysSale>
    {
        public CoysSalesWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            :base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<CoysSale>(
                "//table//td",
                3,
                cells => new CoysSale
                {
                    Description = HttpUtility.HtmlDecode(cells[1].InnerText).Trim(),
                    Price = HttpUtility.HtmlDecode(cells[2].InnerText).Trim()
                }))
        { }

        public CoysSalesWebScraper()
            :this(new HttpLoader(), new DocumentBuilder())
        { }
    }
}
