using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Domain.DTO;
public class CreateAssetDTO
{
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
