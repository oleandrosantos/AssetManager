using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Domain.Entities;

namespace AssetManager.Application.Interfaces;
public interface IAssetEventsService
{
    Task TerminateLoanContract(TerminationLoanAssetModel terminate);
    Task<AssetEventsDTO?> GetAssetEventsById(int idAsset);
    Task<IList<AssetEventsDTO>?> GetLoanAssetsByAsset(int idAsset);
    Task<IList<AssetEventsDTO>?> GetLoanAssetsByCompany(int idCompany);
    Task<IList<AssetEventsDTO>?> GetLoanAssetsByUser(string idUsuario);
    Task LoanAsset(LoanAssetDTO loanAsset);
}