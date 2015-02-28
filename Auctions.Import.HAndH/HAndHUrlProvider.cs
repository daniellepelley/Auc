using System.Collections.Generic;
using Auctions.Model;

namespace Auctions.Import.HAndH
{
    public class HAndHUrlProvider : IUrlProvider
    {
        private readonly string _baseUrl;

        public HAndHUrlProvider(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public IEnumerable<string> GetUrls()
        {
            var page = 1;

            do
            {
                yield return string.Format(_baseUrl, page);
                page++;
            }
            while (page < 10000);
        }
    }
}