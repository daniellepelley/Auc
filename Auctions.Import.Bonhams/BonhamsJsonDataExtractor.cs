using System.Collections.Generic;
using System.Linq;
using System.Web;
using Auctions.Import.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Auctions.Import.Bonhams
{
    public class BonhamsJsonDataExtractor : IJsonDataExtractor<BonhamsSale>
    {
        private readonly JsonValueExtractor _currencyExtractor;
        private readonly JsonValueExtractor _priceExtractor;

        public BonhamsJsonDataExtractor()
        {
            _currencyExtractor = new JsonValueExtractor(HttpUtility.HtmlDecode, "hammer_prices", "prices", 0, "currency");
            _priceExtractor = new JsonValueExtractor("hammer_prices", "prices", 0, "value");
        }

        public BonhamsSale[] Extract(string data)
        {
            var jObject = JObject.Parse(data);
            return jObject["lots"]
                .Select(lot => 
                    new BonhamsSale
                    {
                        Description = lot["sDesc"].ToString(),
                        Currency = _currencyExtractor.GetValue(lot),
                        Price = _priceExtractor.GetValue(lot)
                    })
                .ToArray();
        }
    }
}