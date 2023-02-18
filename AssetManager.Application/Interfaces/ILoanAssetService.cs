using AssetManager.Application.DTO.LoanAsset;
using AssetManager.Domain.Entities;

namespace AssetManager.Application.Interfaces;
public interface ILoanAssetService
{
    Task<LoanAssetDTO> GetLoanByID(string idLoanAsset);
    Task LoanAsset(LoanAssetDTO loanAsset);
    Task DevolutionAsset(TerminationLoanAssetModel terminate);
    Task<IList<LoanAssetDTO>?> GetLoanAssetsByAsset(int idAsset);
    Task<IList<LoanAssetDTO>?> GetLoanAssetsByCompany(int idCompany);
    Task<IList<LoanAssetDTO>?> GetLoanAssetsByUser(string idUser);
}