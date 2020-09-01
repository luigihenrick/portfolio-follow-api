using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class FixedIncomeRequest
    {
        [FromQuery(Name = "percentual_cdi")]
        public decimal CDIPercentage { get; set; }
        [FromQuery(Name = "valor_aplicado")]
        public decimal AppliedAmount { get; set; }
        [FromQuery(Name = "data_emissao")]
        public DateTime IssueDate { get; set; }
    }
}
