using System;

namespace Auctions.Import.Infrastructure
{
    public class Monitor : IMonitor
    {
        private readonly Action<string> _action;

        public Monitor(Action<string> action)
        {
            _action = action;
        }

        public void Update(string message)
        {
            _action(message);
        }
    }
}