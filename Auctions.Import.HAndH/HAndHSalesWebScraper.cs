using System.Web;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.HAndH
{
    public class HAndHSalesWebScraper : HtmlWebScraper<HAndHSale>
    {
        public HAndHSalesWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            :base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<HAndHSale>(
                "//table//tr//td",
                4,
                cells => new HAndHSale
                {
                    Description = HttpUtility.HtmlDecode(cells[2].InnerText),
                    Price = HttpUtility.HtmlDecode(cells[3].InnerText)
                }))
        { }

        public HAndHSalesWebScraper()
            :this(new HttpLoader(), new DocumentBuilder())
        { }
    }
}