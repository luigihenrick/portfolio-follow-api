using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Interfaces;

namespace PortfolioFollow.Controllers
{
    [Route("api/renda-variavel")]
    [ApiController]
    public class VariableIncomeController : ControllerBase
    {
        private readonly IVariableIncomeService variableIncomeService;

        public VariableIncomeController(IVariableIncomeService variableIncomeService)
        {
            this.variableIncomeService = variableIncomeService;
        }

        [HttpGet]
        [Route("preco/{ticker}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(string ticker)
        {
            var result = await variableIncomeService.GetVariableIncomePriceAsync(ticker);

            return Ok(new Asset(result));
        }
    }
}
