using PortfolioFollow.Domain;
using PortfolioFollow.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Api.Models
{
    public class Asset
    {
        public AssetType Type { get; set; }
        public string Symbol { get; set; }
        public decimal ProfitLoss { get; set; }
        public DateTime SettlementDate { get; set; }
        public IEnumerable<AssetPrice> Prices { get; set; }
    }
}
