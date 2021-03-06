﻿using System.Collections.Generic;
using Auctions.Import.Infrastructure;

namespace Auctions.Import.Coys
{
    public class CoysAuctionUrlProvider : IUrlProvider
    {
        public IEnumerable<string> GetUrls()
        {
            return ConfigSettings.AuctionsListUrls;
        }
    }
}