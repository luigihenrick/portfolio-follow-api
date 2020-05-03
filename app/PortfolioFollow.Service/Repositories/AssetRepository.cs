using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;
using PortfolioFollow.Domain;
using PortfolioFollow.Service.Commons;
using Microsoft.Extensions.Options;
using PortfolioFollow.Common.Interfaces;

namespace PortfolioFollow.Service.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly IOptions<GlobalVariables> _config;

        public AssetRepository(IOptions<GlobalVariables> config)
        {
            _config = config;
        }
        
        public Asset Insert(Asset asset)
        {
            using(var conn = new MySqlConnection(_config.Value.DatabaseConnection))
            {
                asset.AssetId = conn.Execute(@"INSERT INTO Asset (Symbol, Name) VALUES (@Symbol, @Name); SELECT LAST_INSERT_ID();", asset);
            }

            return asset;
        }

        public void Insert(IEnumerable<Asset> assets)
        {
            using(var conn = new MySqlConnection(_config.Value.DatabaseConnection))
            {
                conn.Execute(@"INSERT INTO Asset (Symbol, Name) VALUES (@Symbol, @Name)", assets);
            }
        }

        public Asset FindBySymbol(string symbol)
        {
            using (var conn = new MySqlConnection(_config.Value.DatabaseConnection))
            {
                return conn.Query<Asset>("SELECT Id AssetId, Symbol, Name FROM Asset WHERE Symbol = @Symbol", new { Symbol = symbol }).FirstOrDefault();
            }
        }
    }
}
