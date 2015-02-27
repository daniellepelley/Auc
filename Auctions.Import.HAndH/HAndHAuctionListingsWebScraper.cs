using System;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.HAndH
{
    public class HAndHAuctionListingsWebScraper : HtmlWebScraper<HAndHAuctionListing>
    {
        public HAndHAuctionListingsWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            : base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<HAndHAuctionListing>(
                "//table//tr//td",
                4,
                tds => new HAndHAuctionListing
                {
                    Name = tds[0].InnerText,
                    Date = Convert.ToDateTime(tds[1].InnerText),
                    Type = tds[2].InnerText,
                    Url = tds[3].SelectSingleNode(".//a").Attributes["href"].Value
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