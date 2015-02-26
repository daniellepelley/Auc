using Auctions.Import.Infrastructure.Parsers;

namespace Auctions.Model
{
    public abstract class AuctionSaleMapper<T> : ISaleMapper<T>
    {
        protected readonly YearParser YearParser;
        protected readonly MakeParser MakeParser;
        protected readonly PriceParser PriceParser;

        protected AuctionSaleMapper()
        {
            YearParser = new YearParser();
            MakeParser = new MakeParser();
            PriceParser = new PriceParser();
        }

        public abstract AuctionSale Map(T source);
    }
}