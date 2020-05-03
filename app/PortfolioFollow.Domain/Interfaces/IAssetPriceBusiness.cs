using System.Collections.Generic;
using PortfolioFollow.Domain;

namespace PortfolioFollow.Common.Interfaces
{
    public interface IAssetPriceBusiness
    {
        AssetPrice Insert(AssetPrice assetPrice);
        IEnumerable<AssetPrice> FindBySymbol(string symbol);
    }
}