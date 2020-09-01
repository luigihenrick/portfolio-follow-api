using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class FixedIncomeRequest
    {
        [FromQuery(Name = "percentual_cdi")]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public decimal CDIPercentage { get; set; }

        [FromQuery(Name = "valor_aplicado")]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public decimal AppliedAmount { get; set; }

        [FromQuery(Name = "data_emissao")]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public DateTime IssueDate { get; set; }
    }
}
