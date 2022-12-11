using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Domain.Entities;

[Table("tb_company")]
public class CompanyEntity
{
    [Key]
    public int IdCompany { get; set; }

    public string? CompanyName { get; set; }
    [MinLength(14), StringLength(14)]
    public string? Cnpj { get; set; }
    [DefaultValue(true)]
    public bool IsAtiva { get; set; }
    public virtual List<UserEntity> Users { get; set; }
    public virtual IList<AssetEntity> Asset { get; set; }
}