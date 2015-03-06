using System;

namespace Auction.Reporting.DomainModel
{
    public class Sale
    {
        public int Id { get; set; }
        public int? Price { get; set; }
        public DateTime? Date { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
    }
}