using System.Collections.Generic;
using Auctions.DomainModel;

namespace Auctions.Import.HAndH.Model
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