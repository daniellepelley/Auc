using System.Collections.Generic;

namespace Auctions.Export.Data
{
    public class Make
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
