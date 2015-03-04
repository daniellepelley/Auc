using System.Threading.Tasks;

namespace Auctions.DomainModel
{
    public interface IAuctionListingProvider
    {
        Task<AuctionListing[]> GetAuctionListings();
    }
}