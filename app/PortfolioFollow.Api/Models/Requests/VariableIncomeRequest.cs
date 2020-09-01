using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class VariableIncomeRequest
    {
        [FromQuery(Name = "ticker")]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Symbol { get; set; }
    }
}
