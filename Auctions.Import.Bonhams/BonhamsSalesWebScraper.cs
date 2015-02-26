using Auctions.Import.Infrastructure;

namespace Auctions.Import.Bonhams
{
    public class BonhamsSalesWebScraper : JsonWebScraper<BonhamsSale>
    {
        public BonhamsSalesWebScraper(IHttpLoader httpLoader, IJsonDataExtractor<BonhamsSale> dataExtractor)
            : base(httpLoader, dataExtractor)
        { }

        public BonhamsSalesWebScraper()
            : base(new HttpLoader(), new BonhamsJsonDataExtractor())
        { }
    }
}