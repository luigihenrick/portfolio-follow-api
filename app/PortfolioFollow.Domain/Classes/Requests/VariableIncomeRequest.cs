using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class VariableIncomeRequest : RequestBase
    {
        private readonly Regex regexBrStock = new Regex(@"[a-zA-Z]{4,4}[0-9]{1,1}");
        private string symbol;

        public VariableIncomeRequest() : base(AssetType.RV)
        { }

        public string Symbol
        {
            get => symbol;
            set
            {
                if (!string.IsNullOrWhiteSpace(regexBrStock.Match(value).Value))
                    symbol = value + ".SAO";
                else
                    symbol = value;
            }
        }
    }
}
