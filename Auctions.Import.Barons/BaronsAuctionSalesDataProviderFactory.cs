using System;
using Auctions.Import.Barons.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.Model;
using Autofac;

namespace Auctions.Import.Barons
{
    public class BaronsAuctionSalesDataProviderFactory : IAuctionSalesDataProviderFactory
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
            builder.RegisterType<BaronsAuctionListingProvider>().As<IAuctionListingProvider>();
            builder.RegisterType<BaronsSalesWebDataImporter>().As<IWebDataImporter<BaronsSale>>();
            builder.RegisterType<BaronsSalesMapper>().As<ISaleMapper<BaronsSale>>();
            builder.RegisterType<AuctionSalesDataImporter<BaronsSale>>().As<IWebDataImporter<AuctionSale>>();
            builder.RegisterType<AuctionSalesDataProvider>().As<IDataProvider<AuctionSale>>();

            builder.RegisterType<DocumentBuilder>().As<IDocumentBuilder>();
            builder.RegisterType<HttpLoader>().As<IHttpLoader>();

            return builder;
        }

    }
}