using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Domain.Classes.Requests;

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
        public async Task<IActionResult> GetAsync(string codigo)
        {
            var result = await variableIncomeService.GetPriceAsync(new VariableIncomeRequest { Symbol = codigo });

            return Ok(new Asset(result));
        }
    }
}
