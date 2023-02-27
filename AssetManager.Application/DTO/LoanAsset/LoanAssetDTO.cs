using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.LoanAsset;
public class LoanAssetDTO : AssetEventsDTO
{
    public LoanAssetDTO()
    {
        this.EventType = (int)Enums.EventsType.Loan;
    }

}

public class AssetEventsDTO
{
    public int IdEvent { get; set; }

    [Required]
    public DateTime EventDate { get; set; }
    public string? Description { get; set; }
    [Required]
    public string IdUser { get; set; }
    [Required]
    public string IdUserRegister { get; set; }
    [Required]
    public int IdAsset { get; set; }
    [Required]
    public int IdCompany { get; set; }
    public int EventType { get; set; }
}