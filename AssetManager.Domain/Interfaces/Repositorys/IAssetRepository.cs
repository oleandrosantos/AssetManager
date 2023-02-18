using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys
{
    public interface IAssetRepository : IRepositoryBase<AssetEntity>
    {
        Task<AssetEntity?> GetBySKU(string SKU);
        Task<IList<AssetEntity>> GetAssetsByCompany(int idCompany);
        Task Delete(int id, string? ExclusionInfo);
        Task<AssetEntity?> GetById(int id);
    }
}
