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
    [Required]
    public DateTime LoanDate { get; set; }
    public DateTime? DevolutionDate { get; set; }
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