using System.Collections.Generic;
using Auctions.Model;

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