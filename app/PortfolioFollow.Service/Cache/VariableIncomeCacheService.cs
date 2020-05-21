using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes.Requests;
using PortfolioFollow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioFollow.Service.Cache
{
    public class VariableIncomeCacheService : IVariableIncomeCacheService
    {
        public Task<IEnumerable<AssetPrice>> GetAllPriceAsync(VariableIncomeRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AssetPrice> GetPriceAsync(VariableIncomeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
