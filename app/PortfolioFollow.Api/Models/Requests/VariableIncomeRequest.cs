using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class VariableIncomeRequest
    {
        [FromQuery(Name = "ticker")]
        public string Symbol { get; set; }
    }
}
