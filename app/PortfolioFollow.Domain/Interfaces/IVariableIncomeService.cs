using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioFollow.Domain.Interfaces
{
    public interface IVariableIncomeService
    {
        Task<AssetPrice> GetVariableIncomePriceAsync(string symbol);
        Task<IEnumerable<AssetPrice>> GetVariableIncomePriceDailyAsync(string symbol);
    }
}
