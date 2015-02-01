using System.Collections.Generic;
using System.Linq;

namespace Auctions.Import.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<List<T>> InSetsOf<T>(this IEnumerable<T> source, int max)
        {
            var toReturn = new List<T>(max);
            foreach (var item in source)
            {
                toReturn.Add(item);
                if (toReturn.Count != max)
                {
                    continue;
                }
                yield return toReturn;
                toReturn = new List<T>(max);
            }
            if (toReturn.Any())
            {
                yield return toReturn;
            }
        }
    }
}
