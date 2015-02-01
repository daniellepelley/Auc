using System;
using System.Linq;
using Auctions.Import.Infrastructure;
using HtmlAgilityPack;

namespace Auctions.Import.HAndH
{
    public class AuctionListingsWebScraper : WebScraper<AuctionListing>
    {
        public AuctionListingsWebScraper(IHtmlLoader htmlLoader, IDocumentBuilder documentBuilder)
            :base(htmlLoader, documentBuilder)
        { }

        public AuctionListingsWebScraper()
            :this(new HtmlLoader(), new DocumentBuilder())
        { }

        protected override AuctionListing[] GetData(HtmlDocument htmlDocument)
        {
            var rows = htmlDocument.DocumentNode.SelectNodes("//table//tr");

            if (rows == null ||
                !rows.Any())
            {
                return new AuctionListing[0]; 
            }

            var allTds = rows.AsEnumerable().First().SelectNodes("//td");

            if (allTds == null)
            {
                return new AuctionListing[0];
            }

            return allTds
                .InSetsOf(4)
                .Select(tds => new AuctionListing
            {
                Name = tds[0].InnerText,
                Date = Convert.ToDateTime(tds[1].InnerText),
                Type = tds[2].InnerText, Url = tds[3].SelectSingleNode(".//a").Attributes["href"].Value
            })
            .ToArray();
        }
    }
}