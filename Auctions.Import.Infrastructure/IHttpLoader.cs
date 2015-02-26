using System;
using System.Threading.Tasks;

namespace Auctions.Import.Infrastructure
{
    public interface IHttpLoader : IDisposable
    {
        Task<string> Load(string url);
    }
}