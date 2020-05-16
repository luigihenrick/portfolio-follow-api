using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioFollow.Domain;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Common.Interfaces;
using PortfolioFollow.Domain.Classes;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text;

namespace PortfolioFollow.Controllers
{
    [Route("api/asset-price")]
    [ApiController]
    public class AssetPriceController : ControllerBase
    {
        private readonly IAssetPriceBusiness _assetPriceBusiness;
        private readonly IOptions<Configurations> _config;

        public AssetPriceController(IAssetPriceBusiness assetPriceBusiness, IOptions<Configurations> config)
        {
            _assetPriceBusiness = assetPriceBusiness;
            _config = config;
        }

        [HttpGet("type/{type}/symbol/{symbol}")]
        public ActionResult<string> Get(AssetType type, string symbol)
        {
            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new StringEnumConverter() },
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                    {
                        OverrideSpecifiedNames = false
                    }
                }
            };

            return JsonConvert.SerializeObject(_assetPriceBusiness.FindPrice(type, symbol), settings);
        }

        [HttpGet]
        public async Task<string> GetHtmlAsync()
        {
            HttpClient client = new HttpClient();

            var requestReturn = await client.GetStringAsync("http://www.tesouro.gov.br/web/stn/tesouro-direto-precos-e-taxas-dos-titulos");

            var sb = new StringBuilder();

            var regexLine = new Regex(@"<tr[\s\S]*?<\/tr>");

            foreach (var line in regexLine.Matches(requestReturn))
            {
                var regexElement = new Regex(@"<td[^>](.+?)<\/td>");

                var elements = regexElement.Matches(line.ToString());

                if (elements.Count() == 4)
                {
                    var regexContent = new Regex(@">([^<]*)<");

                    sb.Append($"Nome: {regexContent.Match(elements[0].Value).Value.Replace(">", "").Replace("<", "")} ");
                    sb.Append($"Vencimento: {regexContent.Match(elements[1].Value).Value.Replace(">", "").Replace("<", "")} ");
                    sb.Append($"Rendimento: {regexContent.Match(elements[2].Value).Value.Replace(">", "").Replace("<", "")} ");
                    sb.Append($"Preço Unitário: {regexContent.Match(elements[3].Value).Value.Replace(">", "").Replace("<", "")} ");
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        [HttpPost]
        public void Post([FromBody] AssetPrice assetPrice)
        {
            _assetPriceBusiness.Insert(assetPrice);
        }
    }
}
