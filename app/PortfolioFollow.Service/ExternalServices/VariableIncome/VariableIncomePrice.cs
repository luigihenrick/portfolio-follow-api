using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PortfolioFollow.Service.ExternalServices.VariableIncome
{
    public partial class VariableIncomePrice
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, TimeSeriesDaily> TimeSeriesDaily { get; set; }
    }

    public partial class MetaData
    {
        [JsonProperty("1. Information")]
        public string Information { get; set; }

        [JsonProperty("2. Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("3. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }

        [JsonProperty("4. Output Size")]
        public string OutputSize { get; set; }

        [JsonProperty("5. Time Zone")]
        public string TimeZone { get; set; }
    }

    public partial class TimeSeriesDaily
    {
        [JsonProperty("1. open")]
        public string Open { get; set; }

        [JsonProperty("2. high")]
        public string High { get; set; }

        [JsonProperty("3. low")]
        public string Low { get; set; }

        [JsonProperty("4. close")]
        public string Close { get; set; }
    }
}
