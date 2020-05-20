using System.Collections.Generic;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes;

namespace PortfolioFollow.Domain.Interfaces
{
    public interface IAssetPriceRepository
    {
        void Insert(AssetPrice assetPrice);
        IEnumerable<AssetPrice> FindPrice(AssetType type, string symbol);
    }
}