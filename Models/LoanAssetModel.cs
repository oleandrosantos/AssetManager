using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Model;

[Table("tb_loanasset")]
public class LoanAssetModel
{
    [Key]
    [MaxLength(32)]
    public string? idLoanAsset { get; set; }
    [Required]
    public DateTime loanDate { get; set; }
    public DateTime? devolutionDate { get; set; }
    [Required]
    public string? description { get; set; }
    [Required]
    public UserModel usuario { get; set; }
    [Required]
    public AssetModel asset { get; set; }
    public CompanyModel company { get; set; }
    
}