using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Service.ExternalServices.TreasuryDirect.Model
{
    public class TrsrBondMkt
    {
        [JsonProperty("opngDtTm")]
        public DateTime Opening { get; set; }

        [JsonProperty("clsgDtTm")]
        public DateTime Closing { get; set; }

        [JsonProperty("qtnDtTm")]
        public DateTime QtnDtTm { get; set; }

        [JsonProperty("stsCd")]
        public int StatusCode { get; set; }

        [JsonProperty("sts")]
        public string Status { get; set; }
    }

    public class FinIndxs
    {
        [JsonProperty("cd")]
        public int Code { get; set; }

        [JsonProperty("nm")]
        public string Name { get; set; }
    }

    public class TrsrBd
    {
        [JsonProperty("cd")]
        public int Code { get; set; }

        [JsonProperty("nm")]
        public string Name { get; set; }

        [JsonProperty("featrs")]
        public string Description { get; set; }

        [JsonProperty("mtrtyDt")]
        public DateTime MaturityDate { get; set; }

        [JsonProperty("invstmtStbl")]
        public string InvstmtStbl { get; set; }

        [JsonProperty("rcvgIncm")]
        public string RecommendedFor { get; set; }

        [JsonProperty("anulRedRate")]
        public decimal AnnualProfitabilityRate { get; set; }

        [JsonProperty("minRedQty")]
        public decimal MinimumQuantity { get; set; }

        [JsonProperty("untrRedVal")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("minRedVal")]
        public decimal MinimumPrice { get; set; }

        [JsonProperty("isinCd")]
        public string IsinCode { get; set; }

        [JsonProperty("FinIndxs")]
        public FinIndxs Index { get; set; }

        [JsonProperty("wdwlDt")]
        public DateTime Started { get; set; }
    }

    public class TrsrBdTradgList
    {
        [JsonProperty("TrsrBd")]
        public TrsrBd Asset { get; set; }
    }

    public class Response
    {
        [JsonProperty("TrsrBondMkt")]
        public TrsrBondMkt TradeMarket { get; set; }

        [JsonProperty("TrsrBdTradgList")]
        public List<TrsrBdTradgList> AssetList { get; set; }
    }

    public class TreasureDirectResponse
    {
        [JsonProperty("responseStatus")]
        public int ResponseStatus { get; set; }

        [JsonProperty("responseStatusText")]
        public string ResponseStatusText { get; set; }

        [JsonProperty("statusInfo")]
        public string StatusInfo { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }


}
