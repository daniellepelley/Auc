using System;
using Auctions.Import.Bonhams.Model;
using Auctions.Model;

namespace Auctions.Import.Bonhams
{
    public class BonhamsSalesMapperBase : AuctionSaleMapperBase<BonhamsSale>
    {
        public override AuctionSale Map(BonhamsSale source)
        {
            var sale = new AuctionSale
            {
                Make = MakeParser.Parse(source.Description),
                Year = YearParser.Parse(source.Description),
                Sold = source.LotStatus == "SOLD",
                Price = PriceParser.Parse(source.Price),
                Currency = "GBP"
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