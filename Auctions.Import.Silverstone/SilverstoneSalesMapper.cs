﻿using Auctions.Import.Silverstone.Model;
using Auctions.DomainModel;

namespace Auctions.Import.Silverstone
{
    public class SilverstoneSalesMapper : AuctionSaleMapperBase<SilverstoneSale>
    {
        public override AuctionSale Map(SilverstoneSale source)
        {
            var description = source.Description;
            var price = source.Price;

            var sale = new AuctionSale
            {
                Year = YearParser.Parse(description),
                Make = MakeParser.Parse(description),
                Currency = "GBP"
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
                    auctionSale.Price = PriceParser.Parse(price);
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