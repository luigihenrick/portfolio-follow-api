using PortfolioFollow.Domain.Classes.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioFollow.Domain.Interfaces
{
    public interface IFixedIncomeCacheService : IExternalServiceBase<FixedIncomeServiceRequest>
    { }

    public interface ITreasureDirectCacheService : IExternalServiceBase<TreasureDirectServiceRequest>
    { }

    public interface IVariableIncomeCacheService : IExternalServiceBase<VariableIncomeServiceRequest>
    { }
}
