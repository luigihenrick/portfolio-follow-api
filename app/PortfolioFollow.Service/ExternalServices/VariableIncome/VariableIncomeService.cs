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

namespace PortfolioFollow.Service.ExternalServices.VariableIncome
{
    public class VariableIncomeService : IVariableIncomeService
    {
        public async Task<AssetPrice> GetVariableIncomePriceAsync(string symbol)
        {
            var prices = await GetVariableIncomePriceDailyAsync(symbol);

            return prices.FirstOrDefault();
        }

        public async Task<IEnumerable<AssetPrice>> GetVariableIncomePriceDailyAsync(string symbol)
        {
            var client = new HttpClient();
            var builder = new UriBuilder("https://www.alphavantage.co/query");
            builder.Port = -1;

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["function"] = "TIME_SERIES_DAILY";
            query["symbol"] = "IBM";
            query["apikey"] = "demo";

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
