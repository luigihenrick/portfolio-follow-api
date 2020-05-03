using System.IO;
using CsvHelper;
using PortfolioFollow.Domain;
using PortfolioFollow.Common.Interfaces;

namespace PortfolioFollow.Service.Business
{
    public class AssetBusiness : IAssetBusiness
    {
        private readonly IAssetRepository _assetRepository;

        public AssetBusiness(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }
        
        public Asset FindBySymbol(string symbol)
        {
            return _assetRepository.FindBySymbol(symbol);
        }

        public Asset Insert(Asset asset)
        {
            return _assetRepository.Insert(asset);
        }

        public void Import(Stream fileStream)
        {
            using (var reader = new StreamReader(fileStream))
            using (var csv = new CsvReader(reader))
            {    
                var assets = csv.GetRecords<Asset>();

                _assetRepository.Insert(assets);
            }
        }
    }
}