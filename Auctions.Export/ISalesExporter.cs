using Auctions.Model;

namespace Auctions.Export
{
    public interface ISalesExporter
    {
        void Export(AuctionSale[] auctionSales);
    }
}