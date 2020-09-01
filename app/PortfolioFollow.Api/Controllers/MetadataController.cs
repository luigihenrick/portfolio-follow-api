using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Domain.Extensions;

namespace PortfolioFollow.Api.Controllers
{
    [Route("api/metadata")]
    [ApiController]
    public class MetadataController : Controller
    {
        [HttpGet]
        [Route("tipo-ativos")]
        [Produces("application/json")]
        public IActionResult AssetTypes()
        {
            var validValues = EnumExtensions.GetValues<AssetType>();

            return Ok(validValues.Select(v => new { Name = v.GetDesciption(), Value = v.ToString() }));
        }
    }
}
