using System.Collections.Generic;
using PortfolioFollow.Domain;

namespace PortfolioFollow.Common.Interfaces
{
    public interface IAssetRepository
    {
        Asset Insert(Asset asset);
        void Insert(IEnumerable<Asset> assets);
        Asset FindBySymbol(string symbol);
    }
}