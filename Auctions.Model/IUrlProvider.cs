using System.Collections.Generic;

namespace Auctions.Model
{
    public interface IUrlProvider
    {
        IEnumerable<string> GetUrls();
    }
}