using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Model;

[Table("tb_loanasset")]
public class LoanAssetModel
{
    [Key]
    [MaxLength(32)]
    public string? IdLoanAsset { get; set; }
    [Required]
    public DateTime LoanDate { get; set; }
    public DateTime? DevolutionDate { get; set; }
    [Required]
    public string? Description { get; set; }
    
    [Required]
    public virtual UserModel Usuario { get; set; }
    
    [Required]
    public virtual AssetModel Asset { get; set; }
    public virtual CompanyModel Company { get; set; }
    
    [ForeignKey("usuario")]
    public string IdUsuario { get; set; }
    
    [ForeignKey("asset")]
    public string IdAsset { get; set; }
    
    [ForeignKey("company")]
    public string IdCompany { get; set; }

}