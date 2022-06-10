using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Model;
[Table("tb_asset")]
public class AssetModel
{
    [Key]
    public int IdAsset { get; set; }
    [Required]
    [MaxLength(100)]
    public string? AssetName { get; set; }
    public int? DepreciationTaxInCents { get; set; }
    public ulong AssetPriceInCents { get; set; }
    
    [Required]
    public DateTime AcquisitionDate { get; set; }
    public DateTime? ExclusionDate { get; set; }
    public string? ExclusionInfos { get; set; }

    [ForeignKey("Company")]
    public int IdCompany { get; set; }
    public virtual CompanyModel Company { get; set; }
    
    [MaxLength(64)]
    public string? Status { get; set; }
    public string? Decription { get; set; }
}