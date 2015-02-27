using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Auctions.Import.Infrastructure;
using HtmlAgilityPack;

namespace Auctions.Import.Silverstone
{
    public class SilverstoneSalesWebScraper : HtmlWebScraper<SilverstoneSale>
    {
        public SilverstoneSalesWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            :base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<SilverstoneSale>(
               @"//div[@class=""lot""]//div[@class=""details""]",
                2,
                CreateSilverstoneSale))
        { }

        public SilverstoneSalesWebScraper()
            :this(new HttpLoader(), new DocumentBuilder())
        { }

        private static SilverstoneSale CreateSilverstoneSale(HtmlNode[] cells)
        {
            var price = HttpUtility.HtmlDecode(cells[1].Elements("div").ElementAt(3).Element("div").InnerText);

            if (!string.IsNullOrEmpty(price))
            {
                price = price.Replace("Sold for (£): ", "£");
            }

            return new SilverstoneSale
            {
                Description = HttpUtility.HtmlDecode(cells[0].Element("h3").Element("a").InnerText),
                Price = price
            };
        }

    }

    public class SilverstoneAuction
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }
    }
}

//https://www.silverstoneauctions.com/past-auctions
//https://www.silverstoneauctions.com/race-retro--classic-car-sale-2015/view_lots/pn/all