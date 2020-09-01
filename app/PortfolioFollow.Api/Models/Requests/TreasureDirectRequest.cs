using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class TreasureDirectRequest
    {
        [FromQuery(Name = "nome")]
        public string Name { get; set; }
    }
}
