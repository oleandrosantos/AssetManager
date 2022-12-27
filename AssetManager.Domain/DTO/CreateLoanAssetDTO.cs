using System.ComponentModel.DataAnnotations;

namespace AssetManager.Domain.DTO;
public class CreateLoanAssetDTO
{
    [Required]
    public DateTime LoanDate { get; set; }
    public DateTime? DevolutionDate { get; set; }
    public string? Description { get; set; }
    [Required]
    public string IdUsuario { get; set; }
    [Required]
    public int IdAsset { get; set; }
    [Required]
    public int IdCompany { get; set; }
}