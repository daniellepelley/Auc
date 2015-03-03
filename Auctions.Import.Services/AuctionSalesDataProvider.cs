using System.Collections.Generic;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.Services
{
    public class AuctionSalesDataProvider : IDataProvider<AuctionSale>
    {
        private readonly IAuctionListingProvider _auctionListingProvider;
        private readonly IWebDataImporter<AuctionSale> _webDataImporter;

        public AuctionSalesDataProvider(IWebDataImporter<AuctionSale> webDataImporter, IAuctionListingProvider auctionListingProvider)
        {
            _webDataImporter = webDataImporter;
            _auctionListingProvider = auctionListingProvider;
        }

        public async Task<AuctionSale[]> GetData()
        {
            var auctionSales = new List<AuctionSale>();

            var auctionListings = await _auctionListingProvider.GetAuctionListings();

            foreach (var auctionListing in auctionListings)
            {
                foreach (var auctionSale in await _webDataImporter.Import(auctionListing.Url))
                {
                    auctionSale.AuctionListing = auctionListing;
                    auctionSales.Add(auctionSale);
                }
            }

            return auctionSales.ToArray();
        }
    }
}