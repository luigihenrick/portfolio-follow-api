using System;
using System.Collections.Generic;
using System.Text;
using PortfolioFollow.Domain;
using PortfolioFollow.Common.Interfaces;
using PortfolioFollow.Domain.Classes;

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
