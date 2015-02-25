using System;
using System.Linq;

namespace Auctions.Import.Infrastructure.Parsers
{
    public class YearParser
    {
        public int? Parse(string year)
        {
            if (year.ToCharArray().All(Char.IsNumber))
            {
                return Convert.ToInt32(year);
            }
            return null;
        }
    }
}