using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;

namespace Auctions.DomainModel
{
    public class AuctionImporter : IWebDataImporter<AuctionSale>
    {
        private readonly IWebDataImporter<AuctionSale>  _salesDataImporter;
        private readonly Func<AuctionSale[], bool> _isLastPageFunc;

        public AuctionImporter(IWebDataImporter<AuctionSale> salesDataImporter, Func<AuctionSale[], bool> isLastPageFunc)
        {
            _isLastPageFunc = isLastPageFunc;
            _salesDataImporter = salesDataImporter;
        }

        public async Task<AuctionSale[]> Import(string url)
        {
            var urlProvider = new IncrementalUrlProvider(url);

            var output = new List<AuctionSale>();

            foreach (var u in urlProvider.GetUrls())
            {
                var sales = await _salesDataImporter.Import(u);

                output.AddRange(sales);

                if (_isLastPageFunc(sales))
                {
                    break;
                }
            }
            return output.ToArray();
        }
    }
}