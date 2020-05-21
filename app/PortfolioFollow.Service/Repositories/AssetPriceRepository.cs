using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Service.Database;

namespace PortfolioFollow.Service.Repositories
{
    public class AssetPriceRepository : IAssetPriceRepository
    {
        public const string DatabaseName = "portfolio-follow";
        private const string CollectionName = "asset-price";
        private readonly IConfiguration config;

        public AssetPriceRepository(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<IEnumerable<AssetPrice>> FindPricesAsync(AssetType type, string symbol)
        {
            var database = new ConnectionFactory(config).GetDatabase();

            return await database.GetCollection<AssetPrice>(CollectionName)
                .Find(a => a.Type == type && a.Symbol == symbol)
                .ToListAsync();
        }

        public async Task InsertOneAsync(AssetPrice assetPrice)
        {
            var database = new ConnectionFactory(config).GetDatabase();

            await database.GetCollection<AssetPrice>(CollectionName).InsertOneAsync(assetPrice);
        }

        public async Task InsertManyAsync(IEnumerable<AssetPrice> assetPrice)
        {
            var database = new ConnectionFactory(config).GetDatabase();

            await database.GetCollection<AssetPrice>(CollectionName).InsertManyAsync(assetPrice);
        }
    }
}
