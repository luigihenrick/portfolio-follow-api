using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioFollow.Domain.Interfaces
{
    public interface ITreasureDirectService
    {
        Task<IEnumerable<AssetPrice>> GetAllTreasureDirectPricesAsync();
    }
}
