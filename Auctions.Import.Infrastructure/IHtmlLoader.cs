using System;

namespace Auctions.Import.Infrastructure
{
    public interface IHtmlLoader : IDisposable
    {
        string Load(string url);
    }
}