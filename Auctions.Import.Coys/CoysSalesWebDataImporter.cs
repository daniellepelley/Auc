using System.Web;
using Auctions.Import.Coys.Model;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.Coys
{
    public class CoysSalesWebDataImporter : HtmlWebDataImporter<CoysSale>
    {
        public CoysSalesWebDataImporter(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            :base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<CoysSale>(
                "//table//td",
                3,
                cells => new CoysSale
                {
                    Description = HttpUtility.HtmlDecode(cells[1].InnerText).Trim(),
                    Price = HttpUtility.HtmlDecode(cells[2].InnerText).Trim()
                }))
        { }

        public CoysSalesWebDataImporter()
            :this(new HttpLoader(), new DocumentBuilder())
        { }
    }
}
