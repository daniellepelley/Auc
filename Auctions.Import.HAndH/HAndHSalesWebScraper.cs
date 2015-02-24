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
            :base(htmlLoader, documentBuilder, new DataExtracter<HAndHSale>(
                "//table//tr//td", 4, cells => new HAndHSale
                {
                    Description = HttpUtility.HtmlDecode(cells[2].InnerText),
                    Price = HttpUtility.HtmlDecode(cells[3].InnerText)
                }
                
                ))
        { }

        public HAndHSalesWebScraper()
            :this(new HtmlLoader(), new DocumentBuilder())
        { }
    }
}