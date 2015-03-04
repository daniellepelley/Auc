using System;
using Auctions.Import.Infrastructure;
using Auctions.DomainModel;

namespace Auctions.Import.Services
{
    public interface IAuctionSalesDataProviderFactory
    {
        IDataProvider<AuctionSale> Create(Func<AuctionListing, bool> filter, IMonitor monitor);
    }
}