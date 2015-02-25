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

        public AuctionSale Map(HAndHSale source)
        {
            var description = source.Description;
            var price = source.Price;

            var sale = new AuctionSale
            {
                Year = _hAndHYearParser.Parse(description),
                Make = _makeParser.Parse(description)
            };

            SetModel(sale, description);
            SetPrice(price, sale);

            return sale;
        }

        private void SetPrice(string price, AuctionSale auctionSale)
        {
            if (!string.IsNullOrEmpty(price) &&
                price != "Not Sold")
            {
                auctionSale.Sold = true;

                if (price != "Sold")
                {
                    auctionSale.Price = _priceParser.Parse(price.Substring(9, price.Length - 9));
                }
            }
            else
            {
                auctionSale.Price = null;
            }
        }

        private void SetModel(AuctionSale auctionSale, string description)
        {
            if (auctionSale.Year.HasValue)
            {
                description = description
                    .Replace(auctionSale.Year.ToString(), string.Empty).Trim();
            }

            if (!string.IsNullOrEmpty(auctionSale.Make))
            {
                description = description
                    .Replace(auctionSale.Make, string.Empty).Trim();
            }

            auctionSale.Model = description;
        }
    }
}