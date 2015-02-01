using System;
using Auctions.Import.Infrastructure.Parsers;

namespace Auctions.Import.HAndH
{
    public class HAndHYearParser
    {
        private readonly YearParser _yearParser;

        public HAndHYearParser()
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