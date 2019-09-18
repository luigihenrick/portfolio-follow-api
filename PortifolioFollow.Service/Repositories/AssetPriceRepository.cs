using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using PortifolioFollow.Domain;
using PortifolioFollow.Service.Commons;

namespace PortifolioFollow.Service.Repositories
{
    public class AssetPriceRepository : IAssetPriceRepository
    {
        private readonly IOptions<GlobalVariables> _config;

        public AssetPriceRepository(IOptions<GlobalVariables> config)
        {
            _config = config;
        }
        
        public IEnumerable<AssetPrice> FindBySymbol(string symbol)
        {
            using(var conn = new MySqlConnection(_config.Value.DatabaseConnection))
            {
                return conn
                    .Query<AssetPrice>(
                        @"  SELECT ASP.`AssetPriceId`, ASP.`AssetId`, ASP.`Close`, ASP.`Date` 
                            FROM `AssetPrice` ASP 
                            INNER JOIN `Asset` ASS ON ASS.`Id` = ASP.`AssetId` 
                            WHERE ASS.Symbol = @Symbol", 
                        new { Symbol = symbol });
            }
        }

        public AssetPrice Insert(AssetPrice assetPrice)
        {
            using(var conn = new MySqlConnection(_config.Value.DatabaseConnection))
            {
                assetPrice.AssetPriceId = conn.Execute(@"INSERT INTO `AssetPrice` (`AssetId`, `Close`, `Date`) VALUES (@AssetId, @Close, @Date); SELECT LAST_INSERT_ID();", assetPrice);
            }

            return assetPrice;
        }
    }
}
