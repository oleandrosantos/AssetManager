using System.ComponentModel.DataAnnotations;

namespace AssetManager.ViewModel
{
    public class CreateAsset
    {
        [Required]
        public string? assetName { get; set; }
        public int? depreciationTaxInCents { get; set; }
        [Required]
        public ulong assetPriceInCents { get; set; }
        [Required]
        public DateTime acquisitionDate { get; set; }
        public DateTime exclusionDate { get; set; }
        public string? exclusionInfos { get; set; }
        [Required]
        public int idCompany { get; set; }
    }
}
