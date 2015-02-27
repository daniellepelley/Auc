using System;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.Coys
{
    public class CoysAuctionsWebScraper : HtmlWebScraper<CoysAuction>
    {
        public CoysAuctionsWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            : base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<CoysAuction>(
                "//table//td",
                3,
                tds => new CoysAuction
                {
                    Name = tds[0].InnerText,
                    Date = Convert.ToDateTime(tds[1].InnerText),
                    //Id = tds[2].InnerText
                }
                ))
        {
        }

        public CoysAuctionsWebScraper()
            : this(new HttpLoader(), new DocumentBuilder())
        {
        }
    }
}