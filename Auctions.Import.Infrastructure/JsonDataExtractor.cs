using System.Linq;
using Auctions.Import.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Auctions.Import.Bonhams
{
    public abstract class JsonDataExtractor<T> : IJsonDataExtractor<T>
    {
        private readonly JTokenExtractor _listExtractor;

        protected JsonDataExtractor(JTokenExtractor listExtractor)
        {
            _listExtractor = listExtractor;
        }

        public T[] Extract(string data)
        {
            var jObject = JObject.Parse(data);
            return _listExtractor.GetValue(jObject)
                .Select(CreateItem)
                .ToArray();
        }

        protected abstract T CreateItem(JToken jToken);
    }
}