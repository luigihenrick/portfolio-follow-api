using System;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Interfaces;

namespace PortfolioFollow.Controllers
{
    [Route("api/preco")]
    [ApiController]
    public class AssetPriceController : ControllerBase
    {
        private readonly IAssetPriceBusiness _assetPriceBusiness;
        private readonly IFixedIncomeService _fixedIncomeService;
        private readonly IOptions<Configurations> _config;

        public AssetPriceController(IAssetPriceBusiness assetPriceBusiness, IFixedIncomeService fixedIncomeService, IOptions<Configurations> config)
        {
            _assetPriceBusiness = assetPriceBusiness;
            _fixedIncomeService = fixedIncomeService;
            _config = config;
        }

        [HttpGet("renda-variavel/ticker/{ticker}")]
        public IActionResult Get(string ticker)
        {
            var result = new Asset(_assetPriceBusiness.FindPrice(AssetType.RV, ticker));

            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("tesouro-direto")]
        public async Task<IActionResult> GetFixedIncomeAsync()
        {
            var result = new List<Asset>();
            var client = new HttpClient();

            var requestReturn = await client.GetStringAsync("http://www.tesouro.gov.br/web/stn/tesouro-direto-precos-e-taxas-dos-titulos");

            var regexLine = new Regex(@"<tr[\s\S]*?<\/tr>");

            foreach (var line in regexLine.Matches(requestReturn))
            {
                var regexElement = new Regex(@"<td[^>](.+?)<\/td>");
                var regexClean = new Regex(@"[R$]|>|<");

                var elements = regexElement.Matches(line.ToString());

                if (elements.Count() == 4)
                {
                    var regexContent = new Regex(@">([^<]*)<");

                    var asset = new Asset
                    {
                        Type = AssetType.RF,
                        Symbol = regexClean.Replace(regexContent.Match(elements[0].Value).Value, ""),
                        SettlementDate = DateTime.Parse(regexClean.Replace(regexContent.Match(elements[1].Value).Value, "")),
                        Prices = new List<AssetPrice>
                        {
                            new AssetPrice
                            {
                                Date = DateTime.Now,
                                Price = decimal.Parse(regexClean.Replace(regexContent.Match(elements[3].Value).Value, ""))
                                
                            }
                        },
                        ProfitLoss = decimal.Parse(regexClean.Replace(regexContent.Match(elements[2].Value).Value, "")),
                    };

                    result.Add(asset);
                }
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("renda-fixa")]
        public async Task<IActionResult> GetPrivateFixedIncomeAsync(decimal percentualCdi, decimal valorAplicado, DateTime dataInicio, DateTime? dataFim = null)
        {
            return Ok(await _fixedIncomeService.GetFixedIncomePriceAsync(percentualCdi, valorAplicado, dataInicio, dataFim ?? DateTime.Now));
        }

        [HttpPost]
        public void Post([FromBody]Domain.AssetPrice assetPrice)
        {
            _assetPriceBusiness.Insert(assetPrice);
        }
    }
}
