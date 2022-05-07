using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.ViewModel
{
    public class CreateCompanyViewModel
    {
        public string? companyName { get; set; }
        [MinLength(14), StringLength(14)]
        public string? cnpj { get; set; }
        [DefaultValue(true)]
        public bool ativa { get; set; }
    }
}
