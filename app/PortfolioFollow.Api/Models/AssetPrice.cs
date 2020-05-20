using System;
using Newtonsoft.Json;

namespace PortfolioFollow.Api.Models
{
    public class AssetPrice
    {
        [JsonProperty("valor")]
        public decimal Price { get; set; }
        [JsonProperty("data")]
        public DateTime Date { get; set; }
    }
}
