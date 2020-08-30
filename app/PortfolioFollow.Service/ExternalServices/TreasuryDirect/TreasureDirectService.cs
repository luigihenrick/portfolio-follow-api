using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Domain.Classes.Requests;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Service.ExternalServices.TreasuryDirect.Model;

namespace PortfolioFollow.Service.ExternalServices.TreasuryDirect
{
    public class TreasureDirectService : ITreasureDirectService
    {
        private const string RequestUri = "https://www.tesourodireto.com.br/json/br/com/b3/tesourodireto/service/api/treasurybondsinfo.json";
        private const string RequestUriOld = "http://www.tesouro.gov.br/web/stn/tesouro-direto-precos-e-taxas-dos-titulos";

        public async Task<IEnumerable<AssetPrice>> GetAllPricesAsync(TreasureDirectRequest request)
        {
            var result = new List<TreasureDirectPrice>();
            var client = new HttpClient();

            var requestResult = JsonConvert.DeserializeObject<TreasureDirectResponse>(await client.GetStringAsync(RequestUri));

            foreach (var asset in requestResult.Response.AssetList.Select(al => al.Asset))
            {
                var price = new TreasureDirectPrice
                {
                    Name = asset.Name,
                    MaturityDate = asset.MaturityDate,
                    ProfitabilityRate = asset.AnnualProfitabilityRate,
                    UnitPrice = asset.UnitPrice
                };

                result.Add(price);
            }

            return result.Select(r => new AssetPrice
            {
                Type = AssetType.TD,
                Symbol = r.Name,
                Price = r.UnitPrice,
                Date = DateTime.Today
            });
        }

        public async Task<IEnumerable<AssetPrice>> GetAllPricesOldAsync(TreasureDirectRequest request)
        {
            var result = new List<TreasureDirectPrice>();
            var client = new HttpClient();

            var requestReturn = await client.GetStringAsync(RequestUriOld);

            var regexLine = new Regex(@"<tr[\s\S]*?<\/tr>");

            foreach (var line in regexLine.Matches(requestReturn))
            {
                var regexElement = new Regex(@"<td[^>](.+?)<\/td>");
                var regexClean = new Regex(@"[R$]|>|<");

                var elements = regexElement.Matches(line.ToString());

                if (elements.Count() == 4)
                {
                    var regexContent = new Regex(@">([^<]*)<");

                    var price = new TreasureDirectPrice
                    {
                        Name = regexClean.Replace(regexContent.Match(elements[0].Value).Value, ""),
                        MaturityDate = DateTime.ParseExact(regexClean.Replace(regexContent.Match(elements[1].Value).Value, ""), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        ProfitabilityRate = decimal.Parse(regexClean.Replace(regexContent.Match(elements[2].Value).Value, "")),
                        UnitPrice = decimal.Parse(regexClean.Replace(regexContent.Match(elements[3].Value).Value, ""))
                    };

                    result.Add(price);
                }
            }

            return result.Select(r => new AssetPrice
            {
                Type = AssetType.TD,
                Symbol = r.Name,
                Price = r.UnitPrice,
                Date = DateTime.Today
            });
        }

        public async Task<AssetPrice> GetPriceAsync(TreasureDirectRequest request)
        {
            var all = await GetAllPricesAsync(request);

            return all.FirstOrDefault(a => a.Symbol == request.Name);
        }
    }
}
