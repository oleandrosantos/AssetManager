using System.ComponentModel.DataAnnotations;

namespace AssetManager.ViewModel
{
    public class CreateLoanAsset
    {
        [Required]
        public DateTime LoanDate { get; set; }
        public DateTime? DevolutionDate { get; set; }
        public string? Description { get; set; }
        [Required]
        public string IdUsuario { get; set; }
        [Required]
        public int IdAsset { get; set; }
    }
}
