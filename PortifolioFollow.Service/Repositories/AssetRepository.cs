using Dapper;
using MySql.Data.MySqlClient;
using PortifolioFollow.Domain;
using PortifolioFollow.Service.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PortifolioFollow.Service.Repositories
{
    public class AssetRepository
    {
        public Asset Insert(Asset asset)
        {
            using(var conn = new MySqlConnection(GlobalVariables.DatabaseConnection))
            {
                asset.AssetId = conn.Execute(@"INSERT INTO Asset (Symbol, Name) VALUES (@Symbol, @Name); SELECT LAST_INSERT_ID();", asset);
            }

            return asset;
        }

        public Asset FindBySymbol(string symbol)
        {
            using (var conn = new MySqlConnection(GlobalVariables.DatabaseConnection))
            {
                return conn.Query<Asset>("SELECT Id AssetId, Symbol, Name FROM Asset WHERE Symbol = @Symbol", new { Symbol = symbol }).FirstOrDefault();
            }
        }
    }
}
