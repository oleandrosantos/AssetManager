using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Domain.Entities;

public class AssetEntity
{
    public int IdAsset { get; set; }
    public string Sku { get; set; }
    public string AssetName { get; set; }
    public int DepreciationTaxInCents { get; set; }
    public ulong AssetPriceInCents { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime AcquisitionDate { get; set; }
    public DateTime? ExclusionDate { get; set; }
    public string? ExclusionInfos { get; set; }
    public int IdCompany { get; set; }
    public CompanyEntity Company { get; set; }
    public ICollection<AssetEventsEntity> AssetEvents { get; set; }
}