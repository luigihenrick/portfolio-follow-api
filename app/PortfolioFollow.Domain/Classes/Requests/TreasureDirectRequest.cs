using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class TreasureDirectRequest : RequestBase
    {
        public TreasureDirectRequest() : base(AssetType.TD)
        { }

        public string Name { get; set; }
    }
}
