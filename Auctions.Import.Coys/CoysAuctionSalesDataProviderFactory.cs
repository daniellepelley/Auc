using System;
using Auctions.Import.Coys.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.Model;
using Autofac;

namespace Auctions.Import.Coys
{
    public class CoysAuctionSalesDataProviderFactory : IAuctionSalesDataProviderFactory
    {
        public IDataProvider<AuctionSale> Create(Func<AuctionListing, bool> filter, IMonitor monitor)
        {
            var builder = CreateContainerBuilder();
            
            builder.RegisterInstance(monitor).As<IMonitor>();
            builder.RegisterInstance(filter).As<Func<AuctionListing, bool>>();
            var container = builder.Build();

            return container.Resolve<IDataProvider<AuctionSale>>();
        }

        private ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CoysAuctionUrlProvider>().As<IUrlProvider>();
            builder.RegisterType<CoysAuctionsWebDataImporter>().As<IWebDataImporter<AuctionListing>>();
            builder.RegisterType<CoysSalesMapper>().As<ISaleMapper<CoysSale>>();
            builder.RegisterType<CoysSalesWebDataImporter>().As<IWebDataImporter<CoysSale>>();
            builder.RegisterType<AuctionListingProvider>().As<IAuctionListingProvider>();
            builder.RegisterType<AuctionSalesDataImporter<CoysSale>>().As<IWebDataImporter<AuctionSale>>();
            builder.RegisterType<AuctionSalesDataProvider>().As<IDataProvider<AuctionSale>>();
            return builder;
        }

    }
}