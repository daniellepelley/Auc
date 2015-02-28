using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public interface IDataProvider<T>
    {
        Task<T[]> GetData();
    }
}