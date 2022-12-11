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
    public virtual ICollection<UserEntity> Users { get; set; }
    public virtual ICollection<AssetEntity> Asset { get; set; }
    public virtual ICollection<LoanAssetEntity> Loans { get; set; }

}