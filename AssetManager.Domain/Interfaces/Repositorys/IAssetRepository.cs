using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys
{
    public interface IAssetRepository : IRepositoryBase<AssetEntity>
    {
        Task<AssetEntity?> GetBySKU(string SKU);
        Task<IList<AssetEntity>> GetAssetsByCompany(int idCompany);

    }
}
