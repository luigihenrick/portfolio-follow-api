using System.Collections.Generic;
using PortfolioFollow.Domain;

namespace PortfolioFollow.Common.Interfaces
{
    public interface IAssetPriceRepository
    {
        AssetPrice Insert(AssetPrice assetPrice);
        IEnumerable<AssetPrice> FindBySymbol(string symbol);
    }
}