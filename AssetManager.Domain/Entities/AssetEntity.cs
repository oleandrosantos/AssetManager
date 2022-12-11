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
    public string Status { get; set; }
    public int IdCompany { get; set; }
    public virtual CompanyEntity Company { get; set; }
}