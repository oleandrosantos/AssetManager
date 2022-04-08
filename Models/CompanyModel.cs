using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Model;
[Table("tb_company")]
public class CompanyModel
{
    [Key]
    public int idCompany { get; set; }

    public string? companyName
    {
        get => companyName; 
        set => companyName.ToLower().Trim();
    }
    [MinLength(14), StringLength(14)]
    public string? cnpj { get; set; }
    [DefaultValue(true)]
    public bool ativa { get; set; }
}