namespace Auctions.Model
{
    public class AuctionSale
    {
        public string Currency { get; set; }
        public int? Price { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public bool Sold { get; set; }
        public int? Year { get; set; }
    }
}