using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Auctions.Import.Infrastructure;
using HtmlAgilityPack;

namespace Auctions.Import.Barons
{
    public class BaronsSalesWebScraper : WebScraper<BaronsSale>
    {
        public BaronsSalesWebScraper(IHtmlLoader htmlLoader, IDocumentBuilder documentBuilder)
            : base(htmlLoader,
            documentBuilder,
            new DataExtracter<BaronsSale>(
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
            :this(new HtmlLoader(), new DocumentBuilder())
        { }

        private static string Parse(string str)
        {
            return HttpUtility.HtmlDecode(str).Trim();
        }
    }
}