using System.Collections.Generic;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.HAndH
{
    public class HAndHAuctionUrlProvider : IUrlProvider
    {
        private readonly string _baseUrl;

        public HAndHAuctionUrlProvider(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public IEnumerable<string> GetUrls()
        {
            var year = 2015;
            do
            {
                yield return string.Format(_baseUrl, year);
                year--;
            }
            while (year >= 1996);
        }
    }
}