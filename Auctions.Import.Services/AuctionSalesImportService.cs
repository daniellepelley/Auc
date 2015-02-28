using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;
using Auctions.Model;

namespace Auctions.Import.Services
{
    public class AuctionSalesImportService
    {
        private readonly IDataProvider<AuctionSale>[] _dataProviders;

        public AuctionSalesImportService(IDataProvider<AuctionSale>[] dataProviders)
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
