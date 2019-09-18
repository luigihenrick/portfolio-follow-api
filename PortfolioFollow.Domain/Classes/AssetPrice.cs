using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortifolioFollow.Domain
{
    public class AssetPrice
    {
        public int AssetPriceId { get; set; }
        public int AssetId { get; set; }
        public decimal Close { get; set; }
        public DateTime Date { get; set; }
    }
}
