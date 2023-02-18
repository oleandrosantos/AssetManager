using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface ILoanAssetRepository : IRepositoryBase<LoanAssetEntity>
{
    Task<IList<LoanAssetEntity>?> GetByAssetId(int idAsset);
    Task<IList<LoanAssetEntity>?> GetByCompanyId(int idCompany);
    Task<LoanAssetEntity?> GetById(string id);
    Task<IList<LoanAssetEntity>?> GetByUserId(string idUser);
    Task CloseLoan(string id, string Description, DateTime Fi);
}
