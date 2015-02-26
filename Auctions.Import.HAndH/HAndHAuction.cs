using System.Collections.Generic;
using Auctions.Model;

namespace Auctions.Import.HAndH
{
    public class HAndHAuction
    {
        public List<AuctionSale> Sales { get; private set; }

        public HAndHAuction()
        {
            Sales = new List<AuctionSale>();
        }
    }
}