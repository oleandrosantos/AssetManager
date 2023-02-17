using AssetManager.Application.DTO.LoanAsset;

namespace AssetManager.Application.Interfaces;
public interface ILoanAssetService
{
    Task<LoanAssetDTO> GetLoanByID(string idLoanAsset);
    Task LoanAsset(LoanAssetDTO loanAsset);
    Task DevolutionAsset(TerminationLoanAssetModel terminate);
    Task<List<LoanAssetDTO>> GetLoanAssetsByCompany(int idCompany);
    Task<List<LoanAssetDTO>> GetLoanAssetsByAssets(int idAsset);
    Task<List<LoanAssetDTO>> GetActiveLoansByAssets(int idAsset);
    Task<List<LoanAssetDTO>> GetActiveLoansByCompany(int idCompany);
    Task<List<LoanAssetDTO>> UserAssetLoanList(string idUser);

}