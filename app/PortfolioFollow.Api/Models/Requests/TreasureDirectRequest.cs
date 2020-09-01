using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public class TreasureDirectRequest
    {
        [FromQuery(Name = "nome")]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}
