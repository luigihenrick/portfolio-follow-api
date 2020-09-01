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
    public class VariableIncomeCacheService : IVariableIncomeCacheService
    {
        private readonly IVariableIncomeService variableIncomeService;
        private readonly IAssetPriceRepository assetPriceRepository;

        public VariableIncomeCacheService(IVariableIncomeService variableIncomeService, IAssetPriceRepository assetPriceRepository)
        {
            this.variableIncomeService = variableIncomeService;
            this.assetPriceRepository = assetPriceRepository;
        }

        public async Task<IEnumerable<AssetPrice>> GetAllPricesAsync(VariableIncomeServiceRequest request)
        {
            var assetPricesInDb = await assetPriceRepository.FindPricesBySymbolAsync(request.Type, request.Symbol);

            var lastPriceDate = assetPricesInDb.FirstOrDefault().Date;

            var daysWithoutPrice = lastPriceDate.Subtract(lastPriceDate.GetLastWorkDay()).Days;

            if(daysWithoutPrice == 1)
            {
                var price = await variableIncomeService.GetPriceAsync(request);

                if (price.Price > 0)
                {
                    _ = assetPriceRepository.InsertOneAsync(price);
                }

                assetPricesInDb.Append(price);

                return assetPricesInDb;
            }
            else if (daysWithoutPrice >= 2 || assetPricesInDb.Count() <= 100)
            {
                var allPrices = await variableIncomeService.GetAllPricesAsync(request);

                if (allPrices.Any())
                {
                    var assetPricesToInsert = allPrices.Except(assetPricesInDb);

                    _ = assetPriceRepository.InsertManyAsync(assetPricesToInsert);
                }

                return allPrices;
            }

            return assetPricesInDb;
        }

        public async Task<AssetPrice> GetPriceAsync(VariableIncomeServiceRequest request)
        {
            var assetPricesInDb = await assetPriceRepository.FindPricesBySymbolAsync(request.Type, request.Symbol);

            var lastPrice = assetPricesInDb.FirstOrDefault();

            var daysWithoutPrice = lastPrice == null ? 1 : DateTime.Today.GetLastWorkDay().Subtract(lastPrice.Date.GetLastWorkDay()).Days;

            if (daysWithoutPrice >= 1)
            {
                var actualPrice = await variableIncomeService.GetPriceAsync(request);

                if(actualPrice != null)
                    await assetPriceRepository.InsertOneAsync(actualPrice);

                return actualPrice;
            }

            return lastPrice;
        }
    }
}
