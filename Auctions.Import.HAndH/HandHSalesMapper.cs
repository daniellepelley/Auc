using Auctions.Import.Infrastructure.Parsers;
using Auctions.Model;

namespace Auctions.Import.HAndH
{
    public class HandHSalesMapper : ISaleMapper<HAndHSale>
    {
        private readonly PriceParser _priceParser;
        private readonly HAndHYearParser _hAndHYearParser;
        private readonly MakeParser _makeParser;

        public HandHSalesMapper()
        {
            _priceParser = new PriceParser();
            _hAndHYearParser = new HAndHYearParser();
            _makeParser = new MakeParser();
        }

        public Sale Map(HAndHSale source)
        {
            var description = source.Description;
            var price = source.Price;

            var sale = new Sale
            {
                Year = _hAndHYearParser.Parse(description),
                Make = _makeParser.Parse(description)
            };

            SetModel(sale, description);
            SetPrice(price, sale);

            return sale;
        }

        private void SetPrice(string price, Sale sale)
        {
            if (!string.IsNullOrEmpty(price) &&
                price != "Not Sold")
            {
                sale.Sold = true;

                if (price != "Sold")
                {
                    sale.Price = _priceParser.Parse(price.Substring(9, price.Length - 9));
                }
            }
            else
            {
                sale.Price = null;
            }
        }

        private void SetModel(Sale sale, string description)
        {
            if (sale.Year.HasValue)
            {
                description = description
                    .Replace(sale.Year.ToString(), string.Empty).Trim();
            }

            if (!string.IsNullOrEmpty(sale.Make))
            {
                description = description
                    .Replace(sale.Make, string.Empty).Trim();
            }

            sale.Model = description;
        }
    }
}