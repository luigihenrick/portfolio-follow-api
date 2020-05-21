using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PortfolioFollow.Domain;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Domain.Classes;
using PortfolioFollow.Domain.Interfaces;

namespace PortfolioFollow.Service.Repositories
{
    public class AssetPriceRepository : IAssetPriceRepository
    {
        public const string DatabaseName = "portfolio-follow";
        private readonly IMongoDatabase _database;

        public AssetPriceRepository(IOptions<Configurations> config)
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(config.Value.DatabaseConnection));
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o banco de dados.", ex);
            }
        }

        public IEnumerable<AssetPrice> FindPrice(AssetType type, string symbol)
        {
            return _database.GetCollection<AssetPrice>("asset-price")
                .Find(a => a.Type == type && a.Symbol == symbol)
                .SortByDescending(a => a.Date)
                .ToList();
        }

        public void Insert(AssetPrice assetPrice)
        {
            _database.GetCollection<AssetPrice>("asset-price").InsertOne(assetPrice);
        }
    }
}
