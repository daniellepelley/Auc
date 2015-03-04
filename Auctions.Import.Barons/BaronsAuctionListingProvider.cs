using System.Threading.Tasks;
using Auctions.DomainModel;

namespace Auctions.Import.Barons
{
    public class BaronsAuctionListingProvider : IAuctionListingProvider
    {
        public Task<AuctionListing[]> GetAuctionListings()
        {
            return Task.FromResult(new[]
            {
                new AuctionListing
                {
                    Url = "http://www.barons-auctions.com/fullresults.php"
                }
            });
        }
    }
}