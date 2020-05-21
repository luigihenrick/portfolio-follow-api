using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Interfaces;

namespace PortfolioFollow.Controllers
{
    [Route("api/renda-fixa")]
    [ApiController]
    public class FixedIncomeController : ControllerBase
    {
        private readonly IFixedIncomeService fixedIncomeService;

        public FixedIncomeController(IFixedIncomeService fixedIncomeService)
        {
            this.fixedIncomeService = fixedIncomeService;
        }

        [HttpGet]
        [Route("preco")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFixedIncomeAsync(decimal percentualCdi, decimal valorAplicado, DateTime dataInicio, DateTime? dataFim = null)
        {
            var result = await fixedIncomeService.GetFixedIncomePriceAsync(percentualCdi, valorAplicado, dataInicio, dataFim ?? DateTime.Now);

            return Ok(new Asset(result));
        }
    }
}
