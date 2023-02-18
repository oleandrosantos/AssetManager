namespace AssetManager.Application.DTO.LoanAsset;

public class TerminationLoanAssetModel
{
    public string LoanAssetId { get; set; }
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public string IdUser { get; set; }
    public int IdAsset { get; set; }
    public int IdCompany { get; set; }
}