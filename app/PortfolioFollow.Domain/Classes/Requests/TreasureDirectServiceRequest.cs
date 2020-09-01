using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class TreasureDirectServiceRequest : RequestBase
    {
        public TreasureDirectServiceRequest() : base(AssetType.TD)
        { }

        public string Name { get; set; }
    }
}
