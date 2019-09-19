using System.IO;
using PortfolioFollow.Domain;

namespace PortfolioFollow.Common.Interfaces
{
    public interface IAssetBusiness
    {
        Asset FindBySymbol(string symbol);
        Asset Insert(Asset asset);
        void Import(Stream fileStream);
    }
}