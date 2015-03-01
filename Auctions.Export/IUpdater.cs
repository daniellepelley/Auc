using System.Collections.Generic;

namespace Auctions.Export
{
    public interface IUpdater<in T>
        where T : class
    {
        void Update(IEnumerable<T> entities);
    }
}