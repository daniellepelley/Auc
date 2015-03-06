using System.Linq;
using Auction.Reporting.DomainModel;
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

            _auctionEntities.AuctionHouses.Add(new AuctionHouse
            {
                Name = "Coys"
            });

            _auctionEntities.Auctions.Add(new Auction.Reporting.DomainModel.Auction()
            {
                Name = "Great Cars",
            });

            _auctionEntities.Sales.Add(new Sale
            {
                Price = 1000,
                Model = _auctionEntities.Models.FirstOrDefault(x => x.Name == "7"),
                Auction = _auctionEntities.Auctions.FirstOrDefault(x => x.Name == "Great Cars")
            });

            _auctionEntities.SaveChanges();
            return this;
        }
    }
}