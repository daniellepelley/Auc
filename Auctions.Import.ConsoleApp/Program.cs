using System;
using System.Linq;
using Auctions.Import.Bonhams;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.Model;

namespace Auctions.Import.ConsoleApp
{
    class Program
    {
        private static AuctionSalesImportService _service;

        static void Main(string[] args)
        {
            Init();

            var results = _service.Import().Result;

            Console.WriteLine(results.Count());

            Console.ReadLine();
        }

        private static void Init()
        {
            var jsonWebScraper = new JsonWebScraper<AuctionListing>(new HttpLoader(), new BonhamsAuctionJsonDataExtractor());
            var auctionListingProvider = new AuctionListingProvider(x => x.Date >= new DateTime(2014, 1, 1), jsonWebScraper, new BonhamsAuctionUrlProvider());

            var webScraper = new JsonWebScraper<BonhamsSale>(new HttpLoader(), new BonhamsSaleJsonDataExtractor());

            var auctionSalesScraper = new AuctionSalesScraper<BonhamsSale>(webScraper, new BonhamsSalesMapper());

            var auctionSalesDataProvider = new AuctionSalesDataProvider(auctionSalesScraper, auctionListingProvider);
            _service = new AuctionSalesImportService(auctionSalesDataProvider);
        }

    }
}
