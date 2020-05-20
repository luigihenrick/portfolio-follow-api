using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PortfolioFollow.Domain.Classes;

namespace PortfolioFollow.Api.Models
{
    public class Asset
    {
        public Asset()
        { }

        public Asset(Domain.AssetPrice assetPrice) : this(new List<Domain.AssetPrice> { assetPrice })
        { }

        public Asset(IEnumerable<Domain.AssetPrice> assetPrices)
        {
            if(!assetPrices.Any())
                return;

            var asset = assetPrices.FirstOrDefault();

            Type = asset.Type;
            Symbol = asset.Symbol;
            Prices = assetPrices.ToList().Select(ap => new AssetPrice { Date = ap.Date, Price = ap.Price });
        }

        [JsonProperty("tipo")]
        [EnumDataType(typeof(AssetType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public AssetType Type { get; set; }
        [JsonProperty("nome")]
        public string Symbol { get; set; }
        [JsonProperty("rentabilidade")]
        public decimal? ProfitLoss { get; set; }
        [JsonProperty("vencimento")]
        public DateTime? SettlementDate { get; set; }
        [JsonProperty("precos")]
        public IEnumerable<AssetPrice> Prices { get; set; }
    }
}
