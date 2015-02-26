using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public interface IWebScraper<T>
    {
        Task<T[]> Import(string url);
    }
}