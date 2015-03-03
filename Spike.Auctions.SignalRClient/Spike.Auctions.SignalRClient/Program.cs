using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Spike.Auctions.SignalRClient
{
    class Program
    {
        private static IHubProxy _hubProxy;

        static void Main(string[] args)
        {
            string url = @"http://localhost:8080/";
            var connection = new HubConnection(url);
            _hubProxy = connection.CreateHubProxy("ImportHub");
            connection.Start().Wait();

            _hubProxy.On("UpdateProgress", x => Console.WriteLine("Update:{0}", x));

            do
            {
                var line = Console.ReadLine();
                _hubProxy.Invoke("DetermineLength", line);
            } while (true);
        }
    }
}
