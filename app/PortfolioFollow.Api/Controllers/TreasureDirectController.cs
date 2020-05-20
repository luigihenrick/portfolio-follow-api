using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Api.Models;

namespace PortfolioFollow.Controllers
{
    [Route("api/tesouro-direto")]
    [ApiController]
    public class TreasureDirectController : ControllerBase
    {
        private readonly ITreasureDirectService _treasureDirectService;
        private readonly IOptions<Configurations> _config;

        public TreasureDirectController(
            ITreasureDirectService treasureDirectService, 
            IOptions<Configurations> config)
        {
            _treasureDirectService = treasureDirectService;
            _config = config;
        }

        [HttpGet]
        [Route("preco")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTreasureDirectAsync()
        {
            var result = await _treasureDirectService.GetAllTreasureDirectPricesAsync();

            return Ok(result.Select(r => new Asset(r)));
        }
    }
}
