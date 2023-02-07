using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.Asset;
public class UpdateAssetDTO
{
    [Required]
    public int IdAsset { get; set; }
    [Required]
    public string AssetName { get; set; }
    public int? DepreciationTaxInCents { get; set; }
    [Required]
    public ulong AssetPriceInCents { get; set; }
    [Required]
    public DateTime AcquisitionDate { get; set; }
    public DateTime ExclusionDate { get; set; }
    public string? ExclusionInfos { get; set; }
    [Required]
    public int IdCompany { get; set; }
}
