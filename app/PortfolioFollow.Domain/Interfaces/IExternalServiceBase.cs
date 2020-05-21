using PortfolioFollow.Domain.Classes.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioFollow.Domain.Interfaces
{
    public interface IExternalServiceBase<T> where T : RequestBase 
    {
        Task<AssetPrice> GetPriceAsync(T request);
        Task<IEnumerable<AssetPrice>> GetAllPriceAsync(T request);
    }

    public interface IFixedIncomeService : IExternalServiceBase<FixedIncomeRequest>
    { }

    public interface ITreasureDirectService : IExternalServiceBase<TreasureDirectRequest>
    { }

    public interface IVariableIncomeService : IExternalServiceBase<VariableIncomeRequest>
    { }
}
