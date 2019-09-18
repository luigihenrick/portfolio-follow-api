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
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IOptions<GlobalVariables> _config;

        public AssetController(IAssetRepository assetRepository, IOptions<GlobalVariables> config)
        {
            _assetRepository = assetRepository;
            _config = config;
        }

        [HttpGet("{symbol}")]
        public ActionResult<string> Get(string symbol)
        {
            return JsonConvert.SerializeObject(_assetRepository.FindBySymbol(symbol));
        }

        [HttpPost]
        public void Post([FromBody] Asset asset)
        {
            _assetRepository.Insert(asset);
        }
    }
}
