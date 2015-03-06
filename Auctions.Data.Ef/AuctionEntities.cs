using System.Data.Common;
using System.Data.Entity;
using Auction.Reporting.DomainModel;

namespace Auctions.Data.Ef
{
    public class AuctionEntities : DbContext
    {
        public AuctionEntities()
            : base("name=AuctionEntities")
        {
        }

        public AuctionEntities(DbConnection connection)
            : base(connection, true) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<Make> Makes { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Auction.Reporting.DomainModel.Auction> Auctions { get; set; }
        public virtual DbSet<AuctionHouse> AuctionHouses { get; set; }
    }
}