using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Model;
[Table("tb_asset")]
public class AssetModel
{
    [Key]
    public int idAsset { get; set; }
    [Required]
    [MaxLength(100)]
    public string? assetName { get; set; }
    public int? depreciationTaxInCents { get; set; }
    public ulong assetPriceInCents { get; set; }
    [Required]
    public DateTime acquisitionDate { get; set; }
    public DateTime exclusionDate { get; set; }
    public string? exclusionInfos { get; set; }
}