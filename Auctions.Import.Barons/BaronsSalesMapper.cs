using Auctions.Import.Infrastructure;
using Auctions.Import.Infrastructure.Parsers;

namespace Auctions.Import.Barons
{
    public class BaronsSalesMapper : ISaleMapper<BaronsSale>
    {
        private readonly YearParser _yearParser;
        private readonly PriceParser _priceParser;

        public BaronsSalesMapper()
        {
            _yearParser = new YearParser();
            _priceParser = new PriceParser();
        }

        public Sale Map(BaronsSale source)
        {
            var sale = new Sale
            {
                Year = _yearParser.Parse(source.Year),
                Make = source.Make,
                Model = source.Model,
                Price = _priceParser.Parse(source.Price)
            };

            return sale;
        }
    }
}