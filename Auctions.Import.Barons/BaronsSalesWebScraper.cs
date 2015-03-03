using System.Web;
using Auctions.Import.Barons.Model;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.Barons
{
    public class BaronsSalesWebScraper : HtmlWebScraper<BaronsSale>
    {
        public BaronsSalesWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            : base(httpLoader,
            documentBuilder,
            new HtmlDocumentDataExtracter<BaronsSale>(
                "//table//tr//td",
                5,
                row => new BaronsSale
            {
                AuctionDate = Parse(row[0].InnerText),
                Year = Parse(row[1].InnerText),
                Make = Parse(row[2].InnerText),
                Model = Parse(row[3].InnerText),
                Price = Parse(row[4].InnerText)
            },
            sale => !string.IsNullOrEmpty(sale.Year)))
        { }

        public BaronsSalesWebScraper()
            :this(new HttpLoader(), new DocumentBuilder())
        { }

        private static string Parse(string str)
        {
            return HttpUtility.HtmlDecode(str).Trim();
        }
    }
}