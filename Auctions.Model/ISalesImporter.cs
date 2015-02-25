using System.Threading.Tasks;

namespace Auctions.Model
{
    public interface ISalesImporter
    {
        Task<AuctionSale[]> Import();
    }
}