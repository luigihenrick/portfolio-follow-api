using System;
using System.Collections.Generic;
using System.Text;
using PortfolioFollow.Domain;
using PortfolioFollow.Common.Interfaces;

namespace PortfolioFollow.Service.Business
{
    public class AssetPriceBusiness : IAssetPriceBusiness
    {
        private readonly IAssetPriceRepository _assetPriceRepository;

        public AssetPriceBusiness(IAssetPriceRepository assetPriceRepository)
        {
            _assetPriceRepository = assetPriceRepository;
        }

        public IEnumerable<AssetPrice> FindBySymbol(string symbol)
        {
            return _assetPriceRepository.FindBySymbol(symbol);
        }

        public AssetPrice Insert(AssetPrice assetPrice)
        {
            return _assetPriceRepository.Insert(assetPrice);
        }
    }
}
