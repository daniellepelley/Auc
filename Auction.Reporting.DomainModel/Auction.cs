using System.Collections.Generic;

namespace Auction.Reporting.DomainModel
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public int AuctionHouseId { get; set; }
        public AuctionHouse AuctionHouse { get; set; }
    }
}