using System.Web;
using Auctions.Import.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Auctions.Import.Bonhams
{
    public class BonhamsSaleJsonDataExtractor : JsonDataExtractor<BonhamsSale>
    {
        private readonly JsonValueExtractor _currencyExtractor;
        private readonly JsonValueExtractor _priceExtractor;
        private readonly JsonValueExtractor _descriptionExtractor;
        private readonly JsonValueExtractor _lotStatusExtractor;

        public BonhamsSaleJsonDataExtractor()
            : base(new JTokenExtractor("lots"))
        {
            _descriptionExtractor = new JsonValueExtractor("sDesc");
            _currencyExtractor = new JsonValueExtractor(HttpUtility.HtmlDecode, "hammer_prices", "prices", 0, "currency");
            _priceExtractor = new JsonValueExtractor("hammer_prices", "prices", 0, "value");
            _lotStatusExtractor = new JsonValueExtractor("sLotStatus");
        }

        protected override BonhamsSale CreateItem(JToken jToken)
        {
            return new BonhamsSale
            {
                Description = _descriptionExtractor.GetValue(jToken),
                Currency = _currencyExtractor.GetValue(jToken),
                Price = _priceExtractor.GetValue(jToken),
                LotStatus = _lotStatusExtractor.GetValue(jToken),
            };
        }
    }
}