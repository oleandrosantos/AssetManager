using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.ViewModel
{
    public class CreateCompanyViewModel
    {
        public string? CompanyName { get; set; }
        [MinLength(14), StringLength(14)]
        public string? Cnpj { get; set; }
        [DefaultValue(true)]
        public bool IsAtiva { get; set; }
    }
}
