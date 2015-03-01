using Auctions.Import.Infrastructure;
using Auctions.Import.Infrastructure.Parsers;
using Auctions.Model;
using Newtonsoft.Json.Linq;

namespace Auctions.Import.Bonhams
{
    public class BonhamsAuctionJsonDataExtractor : JsonDataExtractor<AuctionListing>
    {
        private readonly JsonValueExtractor _nameExtractor;
        private readonly JsonValueExtractor _idExtractor;
        private readonly JsonValueExtractor _dateExtractor;
        private readonly DateParser _dateParser;

        public BonhamsAuctionJsonDataExtractor()
            : base(new JTokenExtractor("model_results", "sale", "items"))
        {
            _nameExtractor = new JsonValueExtractor("name_text");
            _idExtractor = new JsonValueExtractor("iSaleNo");
            _dateExtractor = new JsonValueExtractor("dtStartUTC");
            _dateParser = new DateParser();
        }

        protected override AuctionListing CreateItem(JToken jToken)
        {
            const string format = "https://www.bonhams.com/api/v1/lots/{0}/?category=results&length=540&minimal=false&page=1";

            return new AuctionListing
            {
                Url = string.Format(format, _idExtractor.GetValue(jToken)),
                Name = _nameExtractor.GetValue(jToken),
                Date = _dateParser.Parse(_dateExtractor.GetValue(jToken), "yyyy-MM-dd HH:mm")
            };
        }
    }
}