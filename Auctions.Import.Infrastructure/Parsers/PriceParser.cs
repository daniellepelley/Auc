using System;
using System.Linq;
using System.Web;

namespace Auctions.Import.Infrastructure.Parsers
{
    public class PriceParser
    {
        public int? Parse(string price)
        {
            var decodedPrice = HttpUtility.HtmlDecode(price);
            var poundRemovedPrice = decodedPrice
                .Replace("£", string.Empty)
                .Replace(",", string.Empty)
                .Replace(".00", string.Empty);

            if (!string.IsNullOrEmpty(poundRemovedPrice) &&
                poundRemovedPrice.ToCharArray().All(Char.IsNumber))
            {
                return Convert.ToInt32(poundRemovedPrice);
            }
            return null;             
        }
    }
}
