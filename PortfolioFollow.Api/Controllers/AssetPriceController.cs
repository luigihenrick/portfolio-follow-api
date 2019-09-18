using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortifolioFollow.Domain;
using PortifolioFollow.Service.Commons;
using PortifolioFollow.Service.Repositories;

namespace PortifolioFollow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetPriceController : ControllerBase
    {
        private readonly IAssetPriceRepository _assetPriceRepository;
        private readonly IOptions<GlobalVariables> _config;

        public AssetPriceController(IAssetPriceRepository assetPriceRepository, IOptions<GlobalVariables> config)
        {
            _assetPriceRepository = assetPriceRepository;
            _config = config;
        }

        [HttpGet("{symbol}")]
        public ActionResult<string> Get(string symbol)
        {
            return JsonConvert.SerializeObject(_assetPriceRepository.FindBySymbol(symbol));
        }

        [HttpPost]
        public void Post([FromBody] AssetPrice assetPrice)
        {
            _assetPriceRepository.Insert(assetPrice);
        }
    }
}
