using PortfolioFollow.Domain.Classes.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioFollow.Domain.Interfaces
{
    public interface IFixedIncomeCacheService : IExternalServiceBase<FixedIncomeRequest>
    { }

    public interface ITreasureDirectCacheService : IExternalServiceBase<TreasureDirectRequest>
    { }

    public interface IVariableIncomeCacheService : IExternalServiceBase<VariableIncomeRequest>
    { }
}
