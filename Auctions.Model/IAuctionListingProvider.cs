using System.Threading.Tasks;

namespace Auctions.Model
{
    public interface IAuctionListingProvider
    {
        Task<AuctionListing[]> GetAuctionListings();
    }
}