using System;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.HAndH
{
    public class HAndHAuctionListingsWebScraper : HtmlWebScraper<AuctionListing>
    {
        public HAndHAuctionListingsWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            : base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<AuctionListing>(
                "//table//tr//td",
                4,
                tds => new AuctionListing
                {
                    Name = tds[0].InnerText,
                    Date = Convert.ToDateTime(tds[1].InnerText),
                    Url = "http://www.classic-auctions.com" + tds[3].SelectSingleNode(".//a").Attributes["href"].Value
                }
                ))
        {
        }

        public HAndHAuctionListingsWebScraper()
            : this(new HttpLoader(), new DocumentBuilder())
        {
        }
    }
}