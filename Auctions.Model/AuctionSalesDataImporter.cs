using System.Linq;
using System.Threading.Tasks;
using Auctions.Import.Infrastructure;

namespace Auctions.DomainModel
{
    public class AuctionSalesDataImporter<T> : IWebDataImporter<AuctionSale>
    {
        private readonly IWebDataImporter<T> _webDataImporter;
        private readonly ISaleMapper<T> _saleMapper;

        public AuctionSalesDataImporter(IWebDataImporter<T> webDataImporter, ISaleMapper<T> saleMapper)
        {
            _saleMapper = saleMapper;
            _webDataImporter = webDataImporter;
        }

        public async Task<AuctionSale[]> Import(string url)
        {
            var result = await _webDataImporter.Import(url);

            return
                result.Select(_saleMapper.Map)
                .ToArray();
        }
    }
}