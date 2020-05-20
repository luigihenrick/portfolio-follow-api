using System.Collections.Generic;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Domain.Interfaces;

namespace PortfolioFollow.Service.Business
{
    public class AssetPriceBusiness : IAssetPriceBusiness
    {
        private readonly IAssetPriceRepository _assetPriceRepository;

        public AssetPriceBusiness(IAssetPriceRepository assetPriceRepository)
        {
            _assetPriceRepository = assetPriceRepository;
        }

        public IEnumerable<AssetPrice> FindPrice(AssetType type, string symbol)
        {
            return _assetPriceRepository.FindPrice(type, symbol);
        }

        public void Insert(AssetPrice assetPrice)
        {
            _assetPriceRepository.Insert(assetPrice);
        }
    }
}
