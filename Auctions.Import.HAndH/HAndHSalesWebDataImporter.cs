using System.Web;
using Auctions.Import.HAndH.Model;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.HAndH
{
    public class HAndHSalesWebDataImporter : HtmlWebDataImporter<HAndHSale>
    {
        public HAndHSalesWebDataImporter(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            :base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<HAndHSale>(
                "//table//tr//td",
                4,
                cells => new HAndHSale
                {
                    Description = HttpUtility.HtmlDecode(cells[2].InnerText),
                    Price = HttpUtility.HtmlDecode(cells[3].InnerText)
                }))
        { }

        public HAndHSalesWebDataImporter()
            :this(new HttpLoader(), new DocumentBuilder())
        { }
    }
}