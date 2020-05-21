using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes;

namespace PortfolioFollow.Domain.Interfaces
{
    public interface IAssetPriceRepository
    {
        Task InsertOneAsync(AssetPrice assetPrice);
        Task InsertManyAsync(IEnumerable<AssetPrice> assetPrices);
        Task<IEnumerable<AssetPrice>> FindPricesAsync(AssetType type, string symbol);
    }
}