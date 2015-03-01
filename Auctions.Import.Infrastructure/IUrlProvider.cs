using System.Collections.Generic;

namespace Auctions.Import.Infrastructure
{
    public interface IUrlProvider
    {
        IEnumerable<string> GetUrls();
    }
}