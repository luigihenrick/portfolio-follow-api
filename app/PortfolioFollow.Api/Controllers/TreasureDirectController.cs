using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Classes.Requests;

namespace PortfolioFollow.Controllers
{
    [Route("api/tesouro-direto")]
    [ApiController]
    public class TreasureDirectController : ControllerBase
    {
        private readonly ITreasureDirectCacheService treasureDirectCacheService;

        public TreasureDirectController(ITreasureDirectCacheService treasureDirectCacheService)
        {
            this.treasureDirectCacheService = treasureDirectCacheService;
        }

        [HttpGet]
        [Route("preco/{nome}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTreasureDirectAsync(string nome)
        {
            var result = await treasureDirectCacheService.GetPriceAsync(new TreasureDirectRequest { Name = nome });

            return Ok(new Asset(result));
        }

        [HttpGet]
        [Route("preco/disponiveis")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTreasureDirectAsync()
        {
            var result = await treasureDirectCacheService.GetAllPricesAsync(new TreasureDirectRequest());

            return Ok(result.Select(r => new Asset(r)));
        }
    }
}
