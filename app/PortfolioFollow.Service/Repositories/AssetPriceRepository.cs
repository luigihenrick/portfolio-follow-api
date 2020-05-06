using System.Collections.Generic;
using Microsoft.Extensions.Options;
using PortfolioFollow.Domain;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Common.Interfaces;
using MongoDB.Driver;
using System;
using PortfolioFollow.Domain.Classes;

namespace PortfolioFollow.Service.Repositories
{
    public class AssetPriceRepository : IAssetPriceRepository
    {
        public const string DatabaseName = "portifolio-follow";
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
