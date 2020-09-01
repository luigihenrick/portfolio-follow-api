using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes.Requests;
using PortfolioFollow.Domain.Interfaces;
using MongoDB.Driver;
using System.Linq;
using PortfolioFollow.Service.Extensions;

namespace PortfolioFollow.Service.Cache
{
    public class TreasureDirectCacheService : ITreasureDirectCacheService
    {
        private readonly ITreasureDirectService variableIncomeService;
        private readonly IAssetPriceRepository assetPriceRepository;

        public TreasureDirectCacheService(ITreasureDirectService treasureDirectService, IAssetPriceRepository assetPriceRepository)
        {
            this.variableIncomeService = treasureDirectService;
            this.assetPriceRepository = assetPriceRepository;
        }

        public async Task<IEnumerable<AssetPrice>> GetAllPricesAsync(TreasureDirectServiceRequest request)
        {
            var assetPricesInDb = await assetPriceRepository.FindPricesByTypeAsync(request.Type);

            var lastPrice = assetPricesInDb.FirstOrDefault();

            var daysWithoutPrice = lastPrice == null ? 1 : DateTime.Today.GetLastWorkDay().Subtract(lastPrice.Date).Days;

            if (daysWithoutPrice >= 1)
            {
                var allPrices = await variableIncomeService.GetAllPricesAsync(request);

                if (allPrices.Any())
                {
                    var assetPricesToInsert = allPrices.Except(assetPricesInDb);

                    _ = assetPriceRepository.InsertManyAsync(assetPricesToInsert);
                }

                return allPrices;
            }

            return assetPricesInDb.Where(a => a.Date == lastPrice.Date);
        }

        public async Task<AssetPrice> GetPriceAsync(TreasureDirectServiceRequest request)
        {
            var assetPricesInDb = await assetPriceRepository.FindPricesBySymbolAsync(request.Type, request.Name);

            var lastPrice = assetPricesInDb.FirstOrDefault();

            var daysWithoutPrice = lastPrice == null ? 1 : lastPrice.Date.Subtract(lastPrice.Date.GetLastWorkDay()).Days;

            if (daysWithoutPrice >= 1)
            {
                var actualPrice = await variableIncomeService.GetPriceAsync(request);

                if(actualPrice != null)
                    _ = assetPriceRepository.InsertOneAsync(actualPrice);

                return actualPrice;
            }

            return lastPrice;
        }
    }
}
