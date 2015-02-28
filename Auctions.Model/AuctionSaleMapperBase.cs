using Auctions.Import.Infrastructure.Parsers;

namespace Auctions.Model
{
    public abstract class AuctionSaleMapperBase<T> : ISaleMapper<T>
    {
        protected readonly YearParser YearParser;
        protected readonly MakeParser MakeParser;
        protected readonly PriceParser PriceParser;

        protected AuctionSaleMapperBase()
        {
            YearParser = new YearParser();
            MakeParser = new MakeParser();
            PriceParser = new PriceParser();
        }

        public abstract AuctionSale Map(T source);
    }
}