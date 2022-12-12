using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces
{
    public interface IAssetRepository : IRepositoryBase<AssetEntity>
    {
        Task<AssetEntity?> GetBySKU(string SKU);
        Task<IEnumerable<AssetEntity>> AssetsByCompanyList(int idCompany);

    }
}
