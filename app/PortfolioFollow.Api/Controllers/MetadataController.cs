using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Domain.Classes.Requests;
using PortfolioFollow.Domain.Extensions;

namespace PortfolioFollow.Api.Controllers
{
    [Route("api/metadata")]
    [ApiController]
    public class MetadataController : Controller
    {
        private readonly Dictionary<AssetType, Type> requestsParams = new Dictionary<AssetType, Type>
        {
            { AssetType.RF, typeof(FixedIncomeRequest) },
            { AssetType.RV, typeof(VariableIncomeRequest) },
            { AssetType.TD, typeof(TreasureDirectRequest) },
        };

        [HttpGet]
        [Route("teste")]
        [Produces("application/json")]
        public IActionResult Teste()
        {
            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("tipo-ativos")]
        [Produces("application/json")]
        public IActionResult AssetTypes()
        {
            var validValues = EnumExtensions.GetValues<AssetType>();

            return Ok(validValues.Select(v => new { Name = v.GetDesciption(), Value = v.ToString() }));
        }

        [HttpGet]
        [Route("propriedades-requisicao")]
        [Produces("application/json")]
        public IActionResult RequestsParams(AssetType assetType)
        {
            var request = Activator.CreateInstance(requestsParams.FirstOrDefault(rp => rp.Key == assetType).Value);

            return Ok(request);
        }
    }
}
