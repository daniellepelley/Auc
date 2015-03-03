using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Auctions.Import.Infrastructure
{
    public class JsonValueExtractor
    {
        private readonly object[] _parts;
        private readonly Func<string, string> _postProcessor;

        public JsonValueExtractor(params object[] parts)
        {
            _parts = parts;
        }

        public JsonValueExtractor(Func<string, string> postProcessor, params object[] parts)
            :this(parts)
        {
            _postProcessor = postProcessor;
        }

        public string GetValue(JToken jToken)
        {
            var token = jToken;

            foreach (var part in _parts)
            {
                if (part is int)
                {
                    token = token.Count() > (int) part ? token[part] : null;
                }
                else
                {
                    token = token[part];
                }
                if (token == null)
                {
                    return string.Empty;
                }
            }

            var value = token.ToString();

            if (_postProcessor != null)
            {
                value = _postProcessor(value);
            }
            return value;
        }
    }
}