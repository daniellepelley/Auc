using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctions.Import.Bonhams
{
    public class BonhamsSale
    {
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }
        public string LotStatus { get; set; }
    }
}
