using System;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.Import.Silverstone.Model;
using Auctions.Model;
using Autofac;

namespace Auctions.Import.Silverstone
{
    public class SilverstoneAuctionSalesDataProviderFactory : IAuctionSalesDataProviderFactory
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
            builder.RegisterType<SilverstonesAuctionUrlProvider>().As<IUrlProvider>();
            builder.RegisterType<SilverstoneAuctionsWebDataImporter>().As<IWebDataImporter<AuctionListing>>();
            builder.RegisterType<AuctionSalesDataImporter<SilverstoneSale>>().As<IWebDataImporter<AuctionSale>>();
            builder.RegisterType<SilverstoneSalesMapper>().As<ISaleMapper<SilverstoneSale>>();
            builder.RegisterType<SilverstoneSalesWebDataImporter>().As<IWebDataImporter<SilverstoneSale>>();
            builder.RegisterType<AuctionSalesDataImporter<SilverstoneSale>>().As<IWebDataImporter<AuctionSale>>();
            builder.RegisterType<AuctionListingProvider>().As<IAuctionListingProvider>();
            builder.RegisterType<AuctionSalesDataImporter<SilverstoneSale>>().As<IWebDataImporter<AuctionSale>>();
            builder.RegisterType<AuctionSalesDataProvider>().As<IDataProvider<AuctionSale>>();
            return builder;
        }
    }
}