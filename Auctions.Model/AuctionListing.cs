using System;

namespace Auctions.DomainModel
{
    public class AuctionListing
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Url { get; set; }
    }
}