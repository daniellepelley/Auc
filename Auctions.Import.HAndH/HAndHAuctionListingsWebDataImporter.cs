using System;
using Auctions.Import.Infrastructure;
using Auctions.DomainModel;

namespace Auctions.Import.HAndH
{
    public class HAndHAuctionListingsWebDataImporter : HtmlWebDataImporter<AuctionListing>
    {
        public HAndHAuctionListingsWebDataImporter(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
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

        public HAndHAuctionListingsWebDataImporter()
            : this(new HttpLoader(), new DocumentBuilder())
        {
        }
    }
}