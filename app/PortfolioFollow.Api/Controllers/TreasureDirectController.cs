using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Classes.Requests;
using AutoMapper;

namespace PortfolioFollow.Controllers
{
    [Route("api/tesouro-direto")]
    [ApiController]
    public class TreasureDirectController : ControllerBase
    {
        private readonly ITreasureDirectCacheService treasureDirectCacheService;
        private readonly IMapper mapper;

        public TreasureDirectController(ITreasureDirectCacheService treasureDirectCacheService, IMapper mapper)
        {
            this.treasureDirectCacheService = treasureDirectCacheService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("preco")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTreasureDirectAsync([FromQuery] TreasureDirectRequest request)
        {
            var result = await treasureDirectCacheService.GetPriceAsync(mapper.Map<TreasureDirectServiceRequest>(request));

            return Ok(new Asset(result));
        }

        [HttpGet]
        [Route("disponiveis")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTreasureDirectAsync()
        {
            var result = await treasureDirectCacheService.GetAllPricesAsync(new TreasureDirectServiceRequest());

            return Ok(result.Select(r => new Asset(r)));
        }
    }
}
