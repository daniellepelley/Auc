using System;

namespace Auctions.Data.Ef
{
    public class Sale
    {
        public int Id { get; set; }
        public int? Price { get; set; }
        public DateTime? Date { get; set; }
        public short ModelId { get; set; }
        public Model Model { get; set; }
    }
}