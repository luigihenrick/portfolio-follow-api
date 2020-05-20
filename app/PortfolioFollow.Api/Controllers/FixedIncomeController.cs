using System;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Interfaces;
using System.Globalization;

namespace PortfolioFollow.Controllers
{
    [Route("api/renda-fixa")]
    [ApiController]
    public class FixedIncomeController : ControllerBase
    {
        private readonly IFixedIncomeService _fixedIncomeService;
        private readonly IOptions<Configurations> _config;

        public FixedIncomeController(
            IFixedIncomeService fixedIncomeService, 
            IOptions<Configurations> config)
        {
            _fixedIncomeService = fixedIncomeService;
            _config = config;
        }

        [HttpGet]
        [Route("preco")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFixedIncomeAsync(decimal percentualCdi, decimal valorAplicado, DateTime dataInicio, DateTime? dataFim = null)
        {
            var result = await _fixedIncomeService.GetFixedIncomePriceAsync(percentualCdi, valorAplicado, dataInicio, dataFim ?? DateTime.Now);

            return Ok(new Asset(result));
        }
    }
}
