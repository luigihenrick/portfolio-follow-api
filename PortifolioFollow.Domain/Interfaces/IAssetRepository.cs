using PortifolioFollow.Domain;

public interface IAssetRepository
{
    Asset Insert(Asset asset);
    Asset FindBySymbol(string symbol);
}