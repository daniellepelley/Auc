using System.Collections.Generic;

namespace Auction.Reporting.DomainModel
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public Make Make { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}