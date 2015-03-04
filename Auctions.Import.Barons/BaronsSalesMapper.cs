using Auctions.Import.Barons.Model;
using Auctions.DomainModel;

namespace Auctions.Import.Barons
{
    public class BaronsSalesMapper : AuctionSaleMapperBase<BaronsSale>
    {
        public override AuctionSale Map(BaronsSale source)
        {
            var sale = new AuctionSale
            {
                Year = YearParser.Parse(source.Year),
                Make = source.Make,
                Model = source.Model,
                Price = PriceParser.Parse(source.Price),
                Currency = "GBP"
            };

            return sale;
        }
    }
}