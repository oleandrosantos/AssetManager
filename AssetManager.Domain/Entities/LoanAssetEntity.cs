using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Domain.Entities;

[Table("tb_loanasset")]
public class LoanAssetEntity
{
    [Key]
    [MaxLength(32)]
    [Column("IdLoanAsset")]
    public string? IdLoanAsset { get; set; }
    
    [Required]
    public DateTime LoanDate { get; set; }
    public DateTime? DevolutionDate { get; set; }

    [Required]
    public string? Description { get; set; }
    
    [ForeignKey("Usuario")]
    public string IdUsuario { get; set; }
    
    [ForeignKey("Asset")]
    public int IdAsset { get; set; }
    
    [ForeignKey("Company")]
    public int IdCompany { get; set; }

    [Required]
    public virtual UserEntity Usuario { get; set; }
    
    [Required]
    public virtual AssetEntity Asset { get; set; }
    public virtual CompanyEntity Company { get; set; }
}