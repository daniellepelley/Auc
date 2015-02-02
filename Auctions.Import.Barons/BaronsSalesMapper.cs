using Auctions.Import.Infrastructure.Parsers;
using Auctions.Model;

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