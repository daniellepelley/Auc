using System.Collections.Generic;
using System.Linq;
using System.Web;
using Auctions.Import.Infrastructure;
using HtmlAgilityPack;

namespace Auctions.Import.HAndH
{
    public class HAndHSalesWebScraper : WebScraper<HAndHSale>
    {
        public HAndHSalesWebScraper(IHtmlLoader htmlLoader, IDocumentBuilder documentBuilder)
            :base(htmlLoader, documentBuilder)
        { }

        public HAndHSalesWebScraper()
            :this(new HtmlLoader(), new DocumentBuilder())
        { }

        protected override HAndHSale[] GetData(HtmlDocument htmlDocument)
        {
            var sales = new List<HAndHSale>();
            var cells = htmlDocument.DocumentNode.SelectNodes("//table//tr//td");

            if (cells == null)
            {
                return sales.ToArray();
            }

            sales.AddRange(cells
                .InSetsOf(4)
                .Where(tds => tds.Count == 4)
                .Select(tds => new HAndHSale
                {
                    Description = HttpUtility.HtmlDecode(tds[2].InnerText),
                    Price = HttpUtility.HtmlDecode(tds[3].InnerText)
                }));

            return sales.ToArray();
        }
    }
}