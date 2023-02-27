using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface IAssetEventsRepository : IRepositoryBase<AssetEventsEntity>
{
    Task<IList<AssetEventsEntity>> GetAllByAsset(int idAsset);
    Task<AssetEventsEntity> GetByID(int id);
    Task<IList<AssetEventsEntity>?> GetLoansAssetsByAssetId(int idAsset);
    Task<IList<AssetEventsEntity>?> GetLoansAssetsByCompanyId(int idCompany);
    Task<IList<AssetEventsEntity>?> GetLoansAssetsByUserId(string idUser);
    Task LoanAsset(AssetEventsEntity assetEvent);
}