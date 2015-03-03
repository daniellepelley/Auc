namespace Auctions.Import.Infrastructure
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
            if (string.IsNullOrEmpty(data))
            {
                return new T[0];
            }

            var jObject = JObject.Parse(data);
            return _listExtractor.GetValue(jObject)
                .Select(CreateItem)
                .ToArray();
        }

        protected abstract T CreateItem(JToken jToken);
    }
}