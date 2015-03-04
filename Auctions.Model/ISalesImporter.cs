using System.Threading.Tasks;

namespace Auctions.DomainModel
{
    public interface ISalesImporter
    {
        Task<AuctionSale[]> Import();
    }
}