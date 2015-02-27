using Newtonsoft.Json.Linq;

namespace Auctions.Import.Infrastructure
{
    public class JTokenExtractor
    {
        private readonly object[] _parts;

        public JTokenExtractor(params object[] parts)
        {
            _parts = parts;
        }

        public JToken GetValue(JToken jToken)
        {
            var token = jToken;

            foreach (var part in _parts)
            {
                token = token[part];
                if (token == null)
                {
                    return string.Empty;
                }
            }

            return token;
        }
    }
}