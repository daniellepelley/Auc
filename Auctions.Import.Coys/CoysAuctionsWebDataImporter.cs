using System;
using System.Linq;
using Auctions.Import.Infrastructure;
using Auctions.Import.Infrastructure.Parsers;
using Auctions.DomainModel;
using HtmlAgilityPack;

namespace Auctions.Import.Coys
{
    public class CoysAuctionsWebDataImporter : HtmlWebDataImporter<AuctionListing>
    {
        public CoysAuctionsWebDataImporter(IHttpLoader httpLoader, IDocumentBuilder documentBuilder)
            : base(httpLoader, documentBuilder, new HtmlDocumentDataExtracter<AuctionListing>(
                "//table//td",
                3,
                CreateAuction
                ))
        { }

        public CoysAuctionsWebDataImporter()
            : this(new HttpLoader(), new DocumentBuilder())
        { }

        private static AuctionListing CreateAuction(HtmlNode[] nodes)
        {
            var dateParser = new DateParser();

            var href = GetHref(nodes);

            string url = string.Empty;

            if (!string.IsNullOrEmpty(href))
            {
                if (!href.StartsWith("/"))
                {
                    href = "/" + href;   
                }
                url = string.Format("http://www.coys.co.uk{0}", href);
            }

            return new AuctionListing
            {
                Name = nodes[0].InnerText,
                Date = dateParser.Parse(nodes[1].InnerText, "d MMMM yyyy"),
                Url = url
            };
        }

        private static string GetHref(HtmlNode[] nodes)
        {
            var href = nodes[2]
                .Get(x => x.SelectSingleNode("a"))
                .Get(x => x.ChildAttributes("href"))
                .Get(x => x.First())
                .Get(x => x.Value).Value;
            
            return href;
        }
    }

    public class SafeGet<TInput>
        where TInput : class
    {
        private readonly TInput _input;

        public TInput Value
        {
            get { return _input; } 
        }

        public SafeGet(TInput input)
        {
            _input = input;
        }

        public SafeGet<TOutput> Get<TOutput>(Func<TInput, TOutput> func)
            where TOutput : class
        {
            if (_input == null)
            {
                return new SafeGet<TOutput>(null);
            }

            try
            {
                var output = func(_input);

                if (output != null)
                {
                    return new SafeGet<TOutput>(output);
                }
            }
            catch
            {

            }

            return new SafeGet<TOutput>(null);
        }
    }

    public static class Extensions
    {
        public static SafeGet<TOutput> Get<TInput, TOutput>(this TInput source, Func<TInput, TOutput> func)
            where TInput : class
            where TOutput : class
        {
            var safeGet = new SafeGet<TInput>(source);
            return safeGet.Get(func);
        }
    }
}