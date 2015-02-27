using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Auctions.Import.Infrastructure.Parsers
{
    public class YearParser
    {
        public int? Parse(string year)
        {
            if (string.IsNullOrWhiteSpace(year))
            {
                return null;
            }

            if (year.ToCharArray().All(Char.IsNumber))
            {
                return Convert.ToInt32(year);
            }

            var regex = new Regex("[1-2][0-9]{3}");
            year = regex.Match(year).Value;

            if (!string.IsNullOrEmpty(year))
            {
                return Convert.ToInt32(year);
            }

            return null;
        }
    }
}