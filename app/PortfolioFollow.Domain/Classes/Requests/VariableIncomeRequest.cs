using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class VariableIncomeRequest : RequestBase
    {
        public VariableIncomeRequest() : base(AssetType.RV)
        { }

        public string Symbol { get; set; }
    }
}
