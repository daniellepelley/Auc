using System;
using System.Linq;
using System.Threading.Tasks;
using Auctions.Import.Bonhams.Model;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.Bonhams
{
    public class BonhamsUrlProvider : IUrlProvider
    {
        private readonly IWebScraper<BonhamsAuction> _webScraper;
        private readonly string _auctionsListUrl;
        private readonly Func<BonhamsAuction, bool> _filter;

        public BonhamsUrlProvider(Func<BonhamsAuction, bool> filter, IWebScraper<BonhamsAuction> webScraper, string auctionsListUrl)
        {
            _filter = filter;
            _auctionsListUrl = auctionsListUrl;
            _webScraper = webScraper;
        }

        public async Task<string[]> GetUrls()
        {
            var auctions = await _webScraper.Import(_auctionsListUrl);

            var newerAuctions = auctions.Where(_filter).ToArray();

            var format = @"https://www.bonhams.com/api/v1/lots/{0}/?category=results&length=540&minimal=false&page=1";

            return newerAuctions.Select(x => string.Format(format, x.Id)).ToArray();
        }
    }
}