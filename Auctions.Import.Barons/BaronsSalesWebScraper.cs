using System.Linq;
using System.Web;
using Auctions.Import.Infrastructure;
using HtmlAgilityPack;

namespace Auctions.Import.Barons
{
    public class BaronsSalesWebScraper : WebScraper<BaronsSale>
    {
        public BaronsSalesWebScraper(IHtmlLoader htmlLoader, IDocumentBuilder documentBuilder)
            : base(htmlLoader, documentBuilder)
        { }

        public BaronsSalesWebScraper()
            :this(new HtmlLoader(), new DocumentBuilder())
        { }

        protected override BaronsSale[] GetData(HtmlDocument htmlDocument)
        {
            var rows = htmlDocument
                .DocumentNode
                .SelectNodes("//table//tr//td")
                .InSetsOf(5);

            return rows
                .Where(row => row.Count == 5)
                .Select(row => new BaronsSale
            {
                AuctionDate = Parse(row[0].InnerText),
                Year = Parse(row[1].InnerText),
                Make = Parse(row[2].InnerText),
                Model = Parse(row[3].InnerText),
                Price = Parse(row[4].InnerText)
            })
            .Where(sale => !string.IsNullOrEmpty(sale.Year))
            .ToArray();
        }

        private string Parse(string str)
        {
            return HttpUtility.HtmlDecode(str).Trim();
        }
    }
}