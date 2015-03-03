using System;
using Auctions.Import.Infrastructure;
using Auctions.Import.Infrastructure.Parsers;
using Auctions.Model;
using HtmlAgilityPack;

namespace Auctions.Import.Silverstone
{
    public class SilverstoneAuctionsWebDataImporter : HtmlWebDataImporter<AuctionListing>
    {
        private static YearParser _yearParser;

        public SilverstoneAuctionsWebDataImporter(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            : base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<AuctionListing>(
                @"//div[@class=""auction""]//div[@class=""details""]",
                1,
                CreateSilverstoneAuction))
        {
            _yearParser = new YearParser();
        }

        public SilverstoneAuctionsWebDataImporter()
            : this(new HttpLoader(), new DocumentBuilder())
        { }

        private static AuctionListing CreateSilverstoneAuction(HtmlNode[] cells)
        {
            var dateParser = new DateParser();
            var description = cells[0].Element("div").Element("div").InnerText;
            var year = _yearParser.Parse(description);
            var name = description.Split(new[] {year.ToString()}, StringSplitOptions.RemoveEmptyEntries)[0].Trim();

            var href = cells[0].Element("a").Attributes[0].Value + "/view_lots/pn/all";

            var description2 = cells[0].InnerText.Split(new[] { (char)13,(char)10 }, StringSplitOptions.RemoveEmptyEntries);

            DateTime? dateTime = null;

            foreach (var d in description2)
            {
                if (d.Trim().StartsWith("Auction:  "))
                {
                    var dateText = d.Trim()
                        .Replace("Auction:  ", string.Empty)
                        .Replace("onwards", string.Empty)
                        .Trim();

                    dateTime = dateParser.Parse(dateText, "dd MMMM yyyy HH:mm");
                }
            }

            return new AuctionListing
            {
                Name = name,
                Date = dateTime,
                Url = href
            };
        }

    }
}