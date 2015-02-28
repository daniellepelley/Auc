using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Import.Infrastructure.Parsers;
using Auctions.Model;
using HtmlAgilityPack;

namespace Auctions.Import.Coys
{
    public class CoysAuctionsWebScraper : HtmlWebScraper<AuctionListing>
    {
        public CoysAuctionsWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            : base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<AuctionListing>(
                "//table//td",
                3,
                CreateAuction
                ))
        { }

        public CoysAuctionsWebScraper()
            : this(new HttpLoader(), new DocumentBuilder())
        { }

        private static AuctionListing CreateAuction(HtmlNode[] nodes)
        {
            var dateParser = new DateParser();

            var href = nodes[2].SelectSingleNode("a").ChildAttributes("href").First().Value;
            var url = string.Format("http://www.coys.co.uk{0}", href);

            return new AuctionListing
            {
                Name = nodes[0].InnerText,
                Date = dateParser.Parse(nodes[1].InnerText, "d MMMM yyyy"),
                Url = url
            };
        }
    }
}