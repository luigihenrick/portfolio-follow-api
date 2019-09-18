using System.Collections.Generic;
using PortifolioFollow.Domain;

public interface IAssetPriceRepository
{
    AssetPrice Insert(AssetPrice assetPrice);
    IEnumerable<AssetPrice> FindBySymbol(string symbol);
}