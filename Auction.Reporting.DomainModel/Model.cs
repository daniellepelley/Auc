using System.Collections.Generic;

namespace Auctions.Data.Ef
{
    public class Model
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public short MakeId { get; set; }
        public Make Make { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}