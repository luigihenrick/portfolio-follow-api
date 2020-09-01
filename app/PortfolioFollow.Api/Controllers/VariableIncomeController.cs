using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Domain.Classes.Requests;

namespace PortfolioFollow.Controllers
{
    [Route("api/renda-variavel")]
    [ApiController]
    public class VariableIncomeController : ControllerBase
    {
        private readonly IVariableIncomeCacheService variableIncomeCacheService;

        public VariableIncomeController(IVariableIncomeCacheService variableIncomeCacheService)
        {
            this.variableIncomeCacheService = variableIncomeCacheService;
        }

        [HttpGet]
        [Route("preco/{ticker}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(string ticker)
        {
            var result = await variableIncomeCacheService.GetPriceAsync(new VariableIncomeServiceRequest { Symbol = ticker });

            return Ok(new Asset(result));
        }

        [HttpGet]
        [Route("historico/{ticker}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync(string ticker)
        {
            var result = await variableIncomeCacheService.GetAllPricesAsync(new VariableIncomeServiceRequest { Symbol = ticker });

            return Ok(new Asset(result));
        }
    }
}
