using System.Collections.Generic;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;
using Auctions.DomainModel;

namespace Auctions.Import.Services
{
    public class AuctionSalesImportService
    {
        private readonly IDataProvider<AuctionSale>[] _dataProviders;

        public AuctionSalesImportService(params IDataProvider<AuctionSale>[] dataProviders)
        {
            _dataProviders = dataProviders;
        }
        
        public async Task<AuctionSale[]> Import()
        {
            var sales = new List<AuctionSale>();

            foreach (var dataProvider in _dataProviders)
            {
                sales.AddRange(await dataProvider.GetData());
            }

            return sales.ToArray();
        }
    }
}
