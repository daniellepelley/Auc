﻿using Auctions.DomainModel;

namespace Auctions.Export
{
    public interface ISalesExporter
    {
        void Export(AuctionSale[] auctionSales);
    }
}