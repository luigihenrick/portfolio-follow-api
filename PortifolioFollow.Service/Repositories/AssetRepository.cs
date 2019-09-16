using Dapper;
using PortifolioFollow.Domain;
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
            using(var conn = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=PortifolioFollow"))
            {
                asset.AssetId = conn.Execute(@"INSERT INTO [Asset] ([Symbol], [Name]) VALUES (@Symbol, @Name); SELECT CAST(SCOPE_IDENTITY() as int)", asset);
            }

            return asset;
        }

        public Asset FindBySymbol(string symbol)
        {
            using (var conn = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=PortifolioFollow"))
            {
                return conn.Query<Asset>("SELECT [Id], [Symbol], [Name] FROM [Asset] WHERE [Symbol] = @Symbol", new { Symbol = symbol }).FirstOrDefault();
            }
        }
    }
}
