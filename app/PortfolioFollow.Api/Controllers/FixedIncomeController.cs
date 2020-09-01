using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioFollow.Api.Models;
using PortfolioFollow.Domain.Classes.Requests;
using PortfolioFollow.Domain.Interfaces;

namespace PortfolioFollow.Controllers
{
    [Route("api/renda-fixa")]
    [ApiController]
    public class FixedIncomeController : ControllerBase
    {
        private readonly IFixedIncomeService fixedIncomeService;
        private readonly IMapper mapper;

        public FixedIncomeController(IFixedIncomeService fixedIncomeService, IMapper mapper)
        {
            this.fixedIncomeService = fixedIncomeService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("preco")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFixedIncomeAsync([FromQuery] FixedIncomeRequest request)
        {
            var result = await fixedIncomeService.GetPriceAsync(mapper.Map<FixedIncomeServiceRequest>(request));

            return Ok(new Asset(result));
        }
    }
}
