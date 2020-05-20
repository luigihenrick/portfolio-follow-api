using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using PortfolioFollow.Domain.Classes;

namespace PortfolioFollow.Domain
{
    [BsonIgnoreExtraElements]
    public class AssetPrice
    {
        [Required]
        public AssetType Type { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
