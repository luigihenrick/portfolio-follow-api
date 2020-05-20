using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PortfolioFollow.Domain.Classes;

namespace PortfolioFollow.Api.Models
{
    public class Asset
    {
        [JsonProperty("tipo")]
        [EnumDataType(typeof(AssetType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public AssetType Type { get; set; }
        [JsonProperty("nome")]
        public string Symbol { get; set; }
        [JsonProperty("rentabilidade")]
        public decimal ProfitLoss { get; set; }
        [JsonProperty("vencimento")]
        public DateTime SettlementDate { get; set; }
        [JsonProperty("precos")]
        public IEnumerable<AssetPrice> Prices { get; set; }
    }
}
