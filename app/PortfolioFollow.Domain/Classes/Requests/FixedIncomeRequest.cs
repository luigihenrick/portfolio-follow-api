using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class FixedIncomeRequest : RequestBase
    {
        public FixedIncomeRequest() : base(AssetType.RF)
        { }

        public decimal CDIPercentage { get; set; }
        public decimal AppliedAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
