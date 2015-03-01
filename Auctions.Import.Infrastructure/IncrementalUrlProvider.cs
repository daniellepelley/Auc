using System.Collections.Generic;

namespace Auctions.Import.Infrastructure
{
    public class IncrementalUrlProvider : IUrlProvider
    {
        private readonly string _baseUrl;

        public IncrementalUrlProvider(string baseUrl)
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