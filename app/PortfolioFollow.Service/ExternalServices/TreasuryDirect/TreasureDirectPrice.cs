using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Service.ExternalServices.TreasuryDirect
{
    public class TreasureDirectPrice
    {
        public string Name { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal ProfitabilityRate { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
