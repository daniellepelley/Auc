using System.Threading.Tasks;

namespace Auctions.Model
{
    public interface IUrlProvider
    {
        Task<string[]> GetUrls();
    }
}