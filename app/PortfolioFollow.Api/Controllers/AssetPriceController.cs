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

        [HttpPost]
        public void Post([FromBody] AssetPrice assetPrice)
        {
            _assetPriceBusiness.Insert(assetPrice);
        }
    }
}
