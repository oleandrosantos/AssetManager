using AssetManager.Infra.Data.DTO;
using AssetManager.Infra.Data.DTO.Asset;

namespace AssetManager.Application.Interfaces;
public interface IAssetService
{
    Task<ResultOperation> CreateAsset(AssetDTO asset);
    List<AssetDTO> GetAssetsByCompany(int idCompany);
    AssetDTO? GetByID(int idAsset);
    Task<ResultOperation> UpdateAsset(UpdateAssetDTO asset);
}