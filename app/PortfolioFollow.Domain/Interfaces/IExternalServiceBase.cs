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
        Task<IEnumerable<AssetPrice>> GetAllPricesAsync(T request);
    }

    public interface IFixedIncomeService : IExternalServiceBase<FixedIncomeServiceRequest>
    { }

    public interface ITreasureDirectService : IExternalServiceBase<TreasureDirectServiceRequest>
    { }

    public interface IVariableIncomeService : IExternalServiceBase<VariableIncomeServiceRequest>
    { }
}
