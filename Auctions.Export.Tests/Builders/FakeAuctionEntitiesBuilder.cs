using System.Linq;
using Auctions.Data.Ef;
using Effort;

namespace Auctions.Export.Tests.Builders
{
    public class FakeAuctionEntitiesBuilder
    {
        private readonly AuctionEntities _auctionEntities;

        public FakeAuctionEntitiesBuilder()
        {
            _auctionEntities = new AuctionEntities(DbConnectionFactory.CreateTransient());
        }

        public AuctionEntities Build()
        {
            return _auctionEntities;
        }

        public FakeAuctionEntitiesBuilder WithExistingRecords()
        {
            _auctionEntities.Makes.Add(new Make
            {
                Name = "Austin"
            });

            _auctionEntities.Models.Add(new Model
            {
                Name = "7",
                Make = _auctionEntities.Makes.FirstOrDefault(x => x.Name == "Austin")
            });

            _auctionEntities.Sales.Add(new Sale
            {
                Price = 1000,
                Model = _auctionEntities.Models.FirstOrDefault(x => x.Name == "7")
            });

            _auctionEntities.SaveChanges();
            return this;
        }
    }
}