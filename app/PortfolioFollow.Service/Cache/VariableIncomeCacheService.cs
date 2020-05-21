using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Domain.Classes.Requests;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Service.Database;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using PortfolioFollow.Service.Extensions;

namespace PortfolioFollow.Service.Cache
{
    public class VariableIncomeCacheService : IVariableIncomeCacheService
    {
        private readonly IConfiguration config;
        private readonly IVariableIncomeService variableIncomeService;
        private readonly IAssetPriceRepository assetPriceRepository;

        public VariableIncomeCacheService(IConfiguration config, IVariableIncomeService variableIncomeService, IAssetPriceRepository assetPriceRepository)
        {
            this.config = config;
            this.variableIncomeService = variableIncomeService;
            this.assetPriceRepository = assetPriceRepository;
        }

        public async Task<IEnumerable<AssetPrice>> GetAllPricesAsync(VariableIncomeRequest request)
        {
            var assetPricesInDb = await assetPriceRepository.FindPricesAsync(request.Type, request.Symbol);

            var lastPriceDate = assetPricesInDb.FirstOrDefault().Date;

            var daysWithoutPrice = lastPriceDate.Subtract(lastPriceDate.GetLastWorkDay()).Days;

            if (daysWithoutPrice >= 1 || assetPricesInDb.Count() == 1)
            {
                var allPrices = await variableIncomeService.GetAllPricesAsync(request);

                var assetPricesToInsert = allPrices.Except(assetPricesInDb);

                await assetPriceRepository.InsertManyAsync(assetPricesToInsert);

                return allPrices;
            }

            return assetPricesInDb;
        }

        public async Task<AssetPrice> GetPriceAsync(VariableIncomeRequest request)
        {
            var assetPricesInDb = await assetPriceRepository.FindPricesAsync(request.Type, request.Symbol);

            var lastPrice = assetPricesInDb.FirstOrDefault();

            var daysWithoutPrice = lastPrice == null ? 1 : lastPrice.Date.Subtract(lastPrice.Date.GetLastWorkDay()).Days;

            if (daysWithoutPrice >= 1)
            {
                var actualPrice = await variableIncomeService.GetPriceAsync(request);

                await assetPriceRepository.InsertOneAsync(actualPrice);

                return actualPrice;
            }

            return lastPrice;
        }
    }
}
