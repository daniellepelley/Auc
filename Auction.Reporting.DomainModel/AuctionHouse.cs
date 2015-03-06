using System.Collections.Generic;

namespace Auction.Reporting.DomainModel
{
    public class AuctionHouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Auction> Auctions { get; set; }
    }
}