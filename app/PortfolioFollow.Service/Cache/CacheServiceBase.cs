using PortfolioFollow.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Service.Cache
{
    public abstract class CacheServiceBase
    {
        private readonly AssetType type;

        public CacheServiceBase(AssetType type)
        {
            this.type = type;
        }

        public string GetAllKey(string symbol) => $"all-{type.ToString().ToLower()}-{symbol}-{DateTime.Today.ToString("yyyyMMdd")}";
        public string GetSingleKey(string symbol) => $"single-{type.ToString().ToLower()}-{symbol}-{DateTime.Today.ToString("yyyyMMdd")}";
    }
}
