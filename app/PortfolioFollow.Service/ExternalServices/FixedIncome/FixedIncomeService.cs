using System;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Domain.Classes;
using Newtonsoft.Json.Converters;
using PortfolioFollow.Domain.Classes.Requests;
using System.Collections.Generic;

namespace PortfolioFollow.Service.ExternalServices.FixedIncome
{
    public class FixedIncomeService : IFixedIncomeService
    {
        public Task<IEnumerable<AssetPrice>> GetAllPricesAsync(FixedIncomeRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AssetPrice> GetPriceAsync(FixedIncomeRequest request)
        {
            var client = new HttpClient();
            var builder = new UriBuilder("https://calculadorarendafixa.com.br/calculadora/di/calculo");
            builder.Port = -1;

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["dataInicio"] = request.StartDate.ToString("yyyy-MM-dd");
            query["dataFim"] = (request.EndDate ?? DateTime.Today).ToString("yyyy-MM-dd");
            query["percentual"] = request.CDIPercentage.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            query["valor"] = request.AppliedAmount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

            builder.Query = query.ToString();
            string url = builder.ToString();

            var fixedIncomePrice = JsonConvert.DeserializeObject<FixedIncomePrice>(
                await client.GetStringAsync(url),
                new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

            return new AssetPrice
            {
                Type = AssetType.RF,
                Price = fixedIncomePrice.ValorCalculado,
                Date = DateTime.Today
            };
        }
    }
}
