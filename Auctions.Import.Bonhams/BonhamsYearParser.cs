using System;
using Auctions.Import.Infrastructure.Parsers;

namespace Auctions.Import.Bonhams
{
    public class BonhamsYearParser
    {
        private readonly YearParser _yearParser;

        public BonhamsYearParser()
        {
            _yearParser = new YearParser();
        }

        public int? Parse(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return null;
            }

            var year = description.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];

            return _yearParser.Parse(year);
        }
    }
}