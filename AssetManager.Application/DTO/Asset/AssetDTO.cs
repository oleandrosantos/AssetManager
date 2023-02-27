using AssetManager.Application.DTO.AssetEvents;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.Asset;
public class AssetDTO
{
    public int IdAsset { get; set; }
    public string AssetName { get; set; }
    public int? DepreciationTaxInCents { get; set; }
    [Required]
    public ulong AssetPriceInCents { get; set; }
    [Required]
    public string Sku { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public DateTime AcquisitionDate { get; set; }
    public DateTime? ExclusionDate { get; set; }
    public string? ExclusionInfos { get; set; }
    [Required]
    public int IdCompany { get; set; }
    public List<AssetEventsDTO> AssetEvents { get; set; }



    public bool IsLoanable()
    {
        var lastLoanContract = AssetEvents.Where(a => a.EventType == (int)Enums.EventsType.Loan).OrderBy(a => a.EventDate).LastOrDefault();
        var t1 = AssetEvents.Where(a => a.EventDate < lastLoanContract.EventDate && a.EventType == (int)Enums.EventsType.Terminate).ToList();
        return AssetEvents.Where(a => a.EventDate < lastLoanContract.EventDate && a.EventType == (int)Enums.EventsType.Terminate).Any();
    }
}
