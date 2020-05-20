using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioFollow.Domain.Interfaces
{
    public interface IFixedIncomeService
    {
        Task<AssetPrice> GetFixedIncomePriceAsync(decimal percentualCdi, decimal valorAplicado, DateTime dataInicio);
        Task<AssetPrice> GetFixedIncomePriceAsync(decimal percentualCdi, decimal valorAplicado, DateTime dataInicio, DateTime dataFim);
    }
}
