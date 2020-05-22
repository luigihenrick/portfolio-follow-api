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
        Task<IEnumerable<AssetPrice>> FindPricesByTypeAsync(AssetType type);
        Task<IEnumerable<AssetPrice>> FindPricesBySymbolAsync(AssetType type, string symbol);
    }
}