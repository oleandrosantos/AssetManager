using AssetManager.Application.DTO.Asset;

namespace AssetManager.Application.Interfaces;
public interface IAssetService
{
    Task CreateAsset(AssetDTO asset);
    Task<IList<AssetDTO>> GetAssetsByCompany(int idCompany);
    Task<AssetDTO?> GetByID(int idAsset);
    Task UpdateAsset(UpdateAssetDTO asset);
    Task DeleteAsset(int idAsset, string exclusionInfo);
}