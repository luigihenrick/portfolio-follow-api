using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Classes.Requests;

namespace PortfolioFollow.Controllers
{
    [Route("api/tesouro-direto")]
    [ApiController]
    public class TreasureDirectController : ControllerBase
    {
        private readonly ITreasureDirectService treasureDirectService;

        public TreasureDirectController(ITreasureDirectService treasureDirectService)
        {
            this.treasureDirectService = treasureDirectService;
        }

        [HttpGet]
        [Route("preco")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTreasureDirectAsync()
        {
            var result = await treasureDirectService.GetAllPriceAsync(new TreasureDirectRequest());

            return Ok(result.Select(r => new Asset(r)));
        }
    }
}
