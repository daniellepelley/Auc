using System;
using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public interface IHtmlLoader : IDisposable
    {
        Task<string> Load(string url);
    }
}