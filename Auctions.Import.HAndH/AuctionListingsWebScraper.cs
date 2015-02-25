using System;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.HAndH
{
    public class AuctionListingsWebScraper : WebScraper<AuctionListing>
    {
        public AuctionListingsWebScraper(IHtmlLoader htmlLoader, IDocumentBuilder documentBuilder)
            : base(htmlLoader, documentBuilder, new HtmlDocumentDataExtracter<AuctionListing>(
                "//table//tr//td",
                4,
                tds => new AuctionListing
                {
                    Name = tds[0].InnerText,
                    Date = Convert.ToDateTime(tds[1].InnerText),
                    Type = tds[2].InnerText,
                    Url = tds[3].SelectSingleNode(".//a").Attributes["href"].Value
                }
                ))
        {
        }

        public AuctionListingsWebScraper()
            : this(new HtmlLoader(), new DocumentBuilder())
        {
        }
    }
}