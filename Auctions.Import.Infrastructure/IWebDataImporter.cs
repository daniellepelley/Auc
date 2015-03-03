using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public interface IWebDataImporter<T>
    {
        Task<T[]> Import(string url);
    }
}