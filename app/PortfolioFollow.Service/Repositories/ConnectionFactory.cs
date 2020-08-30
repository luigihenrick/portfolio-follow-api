using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Service.Repositories
{
    public class ConnectionFactory
    {
        public const string DatabaseName = "portfolio-follow";
        private readonly IConfiguration config;

        public ConnectionFactory(IConfiguration config)
        {
            this.config = config;
        }

        public IMongoDatabase GetDatabase()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(config["DatabaseConnection"]));

                var mongoClient = new MongoClient(settings);

                return mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o banco de dados.", ex);
            }
        }
    }
}
