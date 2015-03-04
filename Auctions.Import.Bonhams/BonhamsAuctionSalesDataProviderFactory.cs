using System;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.DomainModel;
using Autofac;

namespace Auctions.Import.Bonhams
{
    public class BonhamsAuctionSalesDataProviderFactory : IAuctionSalesDataProviderFactory
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

            builder.RegisterType<BonhamsAuctionJsonDataExtractor>().As<IJsonDataExtractor<AuctionListing>>();
            builder.RegisterType<BonhamsAuctionUrlProvider>().As<IUrlProvider>();
            builder.RegisterType<JsonWebDataImporter<AuctionListing>>().As<IWebDataImporter<AuctionListing>>();
            builder.RegisterType<AuctionListingProvider>().As<IAuctionListingProvider>();


            builder.RegisterType<BonhamsSaleJsonDataExtractor>().As<IJsonDataExtractor<BonhamsSale>>();
            builder.RegisterType<BonhamsSalesMapper>().As<ISaleMapper<BonhamsSale>>();
            builder.RegisterType<AuctionSalesDataProvider>().As<IDataProvider<AuctionSale>>();
            builder.RegisterType<JsonWebDataImporter<BonhamsSale>>().As<IWebDataImporter<BonhamsSale>>();
            builder.RegisterType<AuctionSalesDataImporter<BonhamsSale>>().As<IWebDataImporter<AuctionSale>>();

            builder.RegisterType<HttpLoader>().As<IHttpLoader>();

            return builder;
        }

    }
}