using System.Collections.Generic;

namespace Auctions.Data.Ef
{
    public class Make
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
