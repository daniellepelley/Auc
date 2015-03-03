using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Auctions.Import.ConsoleApp
{
    [HubName("ImportHub")]
    public class ImportHub : Hub
    {
        public void DetermineLength(string message, IProgress<int> progress)
        {
            UpdateProgress(message);
            Console.WriteLine(message);
        }

        public void UpdateProgress(string message)
        {
            Clients.All.UpdateProgress(message);
        }
    }
}