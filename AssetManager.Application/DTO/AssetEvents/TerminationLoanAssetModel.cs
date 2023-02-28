namespace AssetManager.Application.DTO.AssetEvents;

public class TerminationLoanAssetModel
{
    public int IdAsset { get; set; }
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public string IdUserRegister { get; set; }
}