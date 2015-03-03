using System;
using System.Collections.Generic;
using System.Linq;
using Auctions.Import.Bonhams;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Import.Services;
using Auctions.Model;
using Autofac;
using Autofac.Builder;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;

namespace Auctions.Import.ConsoleApp
{
    class Program
    {
        private static AuctionSalesImportService _service;

        static void Main(string[] args)
        {
            Init();

            using (SignalR())
            {
                Console.WriteLine("Return to start...");
                Console.ReadLine();
                var results = _service.Import().Result;

                Console.WriteLine(results.Count());

                Console.ReadLine();
            }
        }

        private static void Init()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BonhamsAuctionSalesDataProviderFactory>()
                .As<IAuctionSalesDataProviderFactory>();

            var container = builder.Build();

            var auctionSalesDataProviderFactories = container.Resolve<IEnumerable<IAuctionSalesDataProviderFactory>>();

            var func = new Func<AuctionListing, bool>(x => x.Date >= new DateTime(2014, 1, 1));

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ImportHub>();

            var monitor = new Monitor(x => hubContext.Clients.All.UpdateProgress(x));

            var dataProviders = auctionSalesDataProviderFactories.Select(x => x.Create(func, monitor)).ToArray();

            _service = new AuctionSalesImportService(dataProviders);
        }

        private static IDisposable SignalR()
        {
            string url = @"http://localhost:8080/";
            var server = WebApp.Start<Startup>(url);
            
            Console.WriteLine(string.Format("Server running at {0}", url));
            return server;
        }

    }
}
