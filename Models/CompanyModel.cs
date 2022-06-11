using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Model;
[Table("tb_company")]
public class CompanyModel
{
    [Key]
    public int IdCompany { get; set; }

    public string? CompanyName { get; set; }
    [MinLength(14), StringLength(14)]
    public string? Cnpj { get; set; }
    [DefaultValue(true)]
    public bool IsAtiva { get; set; }
}