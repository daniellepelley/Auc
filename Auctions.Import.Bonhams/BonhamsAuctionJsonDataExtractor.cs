using System.Web;
using Auctions.Import.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Auctions.Import.Bonhams
{
    public class BonhamsAuctionJsonDataExtractor : JsonDataExtractor<BonhamsAuction>
    {
        private readonly JsonValueExtractor _nameExtractor;
        private JsonValueExtractor _idExtractor;

        public BonhamsAuctionJsonDataExtractor()
            : base(new JTokenExtractor("model_results", "sale", "items"))
        {
            _nameExtractor = new JsonValueExtractor("name_text");
            _idExtractor = new JsonValueExtractor("iSaleNo");
        }

        protected override BonhamsAuction CreateItem(JToken jToken)
        {
            return new BonhamsAuction
            {
                Id = _idExtractor.GetValue(jToken),
                Name = _nameExtractor.GetValue(jToken)
            };
        }
    }
}