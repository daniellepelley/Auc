using System;
using Auctions.Import.Infrastructure.Parsers;
using Auctions.Model;

namespace Auctions.Import.Bonhams
{
    public class BonhamsSalesMapper : ISaleMapper<BonhamsSale>
    {
        private readonly BonhamsYearParser _bonhamsYearParser;
        private readonly MakeParser _makeParser;
        private readonly PriceParser _priceParser;

        public BonhamsSalesMapper()
            :this(new BonhamsYearParser())
        { }

        public BonhamsSalesMapper(BonhamsYearParser bonhamsYearParser)
        {
            _bonhamsYearParser = bonhamsYearParser;
            _makeParser = new MakeParser();
            _priceParser = new PriceParser();
        }

        public AuctionSale Map(BonhamsSale source)
        {
            var sale = new AuctionSale
            {
                Make = _makeParser.Parse(source.Description),
                Year = _bonhamsYearParser.Parse(source.Description),
                Sold = source.LotStatus == "SOLD",
                Price = _priceParser.Parse(source.Price)
            };

            SetModel(sale, source.Description);
            return sale;
        }

        private void SetModel(AuctionSale auctionSale, string description)
        {
            var split = description.Split(new[] {"Chassis no"}, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length == 0)
            {
                return;
            }

            description = split[0];

            if (auctionSale.Year.HasValue)
            {
                split = description.Split(new[] {auctionSale.Year.ToString()}, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length == 2)
                {
                    description = split[1];
                }

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