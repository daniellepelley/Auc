using System.Collections.Generic;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.HAndH
{
    public class Auction
    {
        public List<Sale> Sales { get; set; }

        public Auction()
        {
            Sales = new List<Sale>();
        }
    }
}