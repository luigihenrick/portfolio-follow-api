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
            var result = await treasureDirectService.GetAllPricesAsync(new TreasureDirectRequest());

            return Ok(result.Select(r => new Asset(r)));
        }
    }
}
