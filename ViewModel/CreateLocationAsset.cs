using System.ComponentModel.DataAnnotations;

namespace AssetManager.ViewModel
{
    public class CreateLocationAsset
    {
        [Required]
        public DateTime loanDate { get; set; }
        public DateTime? devolutionDate { get; set; }
        public string? description { get; set; }
        [Required]
        public string idUsuario { get; set; }
        [Required]
        public int idAsset { get; set; }
    }
}
