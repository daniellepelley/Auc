using System;
using System.Web;
using Auctions.Import.Coys.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Infrastructure.Parsers;
using HtmlAgilityPack;

namespace Auctions.Import.Coys
{
    public class CoysAuctionsWebScraper : HtmlWebScraper<CoysAuction>
    {
        public CoysAuctionsWebScraper(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            : base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<CoysAuction>(
                "//table//td",
                3,
                CreateAuction
                ))
        { }

        public CoysAuctionsWebScraper()
            : this(new HttpLoader(), new DocumentBuilder())
        {
        }

        private static CoysAuction CreateAuction(HtmlNode[] nodes)
        {
            var dateParser = new DateParser();
            return new CoysAuction
            {
                Name = nodes[0].InnerText,
                Date = dateParser.Parse(nodes[1].InnerText, "d MMMM yyyy"),
                //Id = tds[2].InnerText
            };
        }
    }
}