using System;
using Auctions.Import.HAndH.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.DomainModel;
using Autofac;

namespace Auctions.Import.HAndH
{
    public class HAndHAuctionSalesDataProviderFactory : IAuctionSalesDataProviderFactory
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
            builder.RegisterType<HAndHAuctionListingsWebDataImporter>().As<IWebDataImporter<AuctionListing>>();
            builder.RegisterType<HandHSalesMapper>().As<ISaleMapper<HAndHSale>>();
            builder.RegisterType<HAndHSalesWebDataImporter>().As<IWebDataImporter<HAndHSale>>();
            builder.RegisterType<AuctionListingProvider>().As<IAuctionListingProvider>();
            builder.RegisterInstance(new HAndHAuctionUrlProvider("http://www.classic-auctions.com/auctions/previous.aspx?year={0}")).As<IUrlProvider>();
            builder.RegisterType<AuctionSalesDataImporter<HAndHSale>>().As<IWebDataImporter<AuctionSale>>();
            builder.RegisterType<AuctionSalesDataProvider>().As<IDataProvider<AuctionSale>>();
            return builder;
        }
    }
}