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

namespace PortfolioFollow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetPriceController : ControllerBase
    {
        private readonly IAssetPriceBusiness _assetPriceBusiness;
        private readonly IOptions<GlobalVariables> _config;

        public AssetPriceController(IAssetPriceBusiness assetPriceBusiness, IOptions<GlobalVariables> config)
        {
            _assetPriceBusiness = assetPriceBusiness;
            _config = config;
        }

        [HttpGet("{symbol}")]
        public ActionResult<string> Get(string symbol)
        {
            return JsonConvert.SerializeObject(_assetPriceBusiness.FindBySymbol(symbol));
        }

        [HttpPost]
        public void Post([FromBody] AssetPrice assetPrice)
        {
            _assetPriceBusiness.Insert(assetPrice);
        }
    }
}
