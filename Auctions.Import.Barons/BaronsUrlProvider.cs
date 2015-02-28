using System.Threading.Tasks;
using Auctions.Model;

namespace Auctions.Import.Barons
{
    public class BaronsUrlProvider : IUrlProvider
    {
        public Task<string[]> GetUrls()
        {
            return Task.FromResult(new[] { "http://www.barons-auctions.com/fullresults.php" });
        }
    }
}