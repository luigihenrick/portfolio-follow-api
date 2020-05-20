using System;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Domain.Classes;
using Newtonsoft.Json.Converters;

namespace PortfolioFollow.Service.ExternalServices.FixedIncome
{
    public class FixedIncomeService : IFixedIncomeService
    {
        public Task<AssetPrice> GetFixedIncomePriceAsync(decimal percentualCdi, decimal valorAplicado, DateTime dataInicio)
        {
            return GetFixedIncomePriceAsync(percentualCdi, valorAplicado, dataInicio, DateTime.Now);
        }

        public async Task<AssetPrice> GetFixedIncomePriceAsync(decimal percentualCdi, decimal valorAplicado, DateTime dataInicio, DateTime dataFim)
        {
            var client = new HttpClient();
            var builder = new UriBuilder("https://calculadorarendafixa.com.br/calculadora/di/calculo");
            builder.Port = -1;

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["dataInicio"] = dataInicio.ToString("yyyy-MM-dd");
            query["dataFim"] = dataFim.ToString("yyyy-MM-dd");
            query["percentual"] = percentualCdi.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            query["valor"] = valorAplicado.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

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
