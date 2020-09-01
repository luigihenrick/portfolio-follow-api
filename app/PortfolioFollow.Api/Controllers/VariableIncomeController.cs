using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Domain.Classes.Requests;
using AutoMapper;

namespace PortfolioFollow.Controllers
{
    [Route("api/renda-variavel")]
    [ApiController]
    public class VariableIncomeController : ControllerBase
    {
        private readonly IVariableIncomeCacheService variableIncomeCacheService;
        private readonly IMapper mapper;

        public VariableIncomeController(IVariableIncomeCacheService variableIncomeCacheService, IMapper mapper)
        {
            this.variableIncomeCacheService = variableIncomeCacheService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("preco")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync([FromQuery] VariableIncomeRequest request)
        {
            var result = await variableIncomeCacheService.GetPriceAsync(mapper.Map<VariableIncomeServiceRequest>(request));

            return Ok(new Asset(result));
        }

        [HttpGet]
        [Route("historico")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync([FromQuery] VariableIncomeRequest request)
        {
            var result = await variableIncomeCacheService.GetAllPricesAsync(mapper.Map<VariableIncomeServiceRequest>(request));

            return Ok(new Asset(result));
        }
    }
}
