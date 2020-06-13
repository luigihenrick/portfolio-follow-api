using System;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Domain.Classes;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using PortfolioFollow.Domain.Classes.Requests;

namespace PortfolioFollow.Service.ExternalServices.VariableIncome
{
    public enum OutputSize
    {
        Full,
        Compact
    }

    public class VariableIncomeService : IVariableIncomeService
    {
        private readonly IConfiguration config;
        private OutputSize outputSize { get; set; }

        public VariableIncomeService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<AssetPrice> GetPriceAsync(VariableIncomeRequest request)
        {
            this.outputSize = OutputSize.Compact;

            var prices = await GetAllPricesAsync(request);

            return prices.FirstOrDefault();
        }

        public async Task<IEnumerable<AssetPrice>> GetAllPricesAsync(VariableIncomeRequest request)
        {
            var client = new HttpClient();
            var builder = new UriBuilder("https://www.alphavantage.co/query");
            builder.Port = -1;

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["apikey"] = this.config["AlphavantageKey"];
            query["function"] = "TIME_SERIES_DAILY";
            query["outputsize"] = this.outputSize.ToString().ToLower();
            query["symbol"] = request.Symbol;

            builder.Query = query.ToString();
            string url = builder.ToString();

            var variableIncomePrice = JsonConvert.DeserializeObject<VariableIncomePrice>(await client.GetStringAsync(url));

            return variableIncomePrice.TimeSeriesDaily.Select(p => new AssetPrice
            {
                Date = DateTime.Parse(p.Key),
                Price = decimal.Parse(p.Value.Close, CultureInfo.InvariantCulture),
                Symbol = variableIncomePrice.MetaData.Symbol,
                Type = AssetType.RV
            });
        }
    }
}
