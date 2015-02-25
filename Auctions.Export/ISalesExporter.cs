namespace Auctions.Export
{
    public interface ISalesExporter
    {
        void Export(Auctions.Model.AuctionSale[] auctionSales);
    }
}