using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Domain.Entities;

public class CompanyEntity
{
    public int IdCompany { get; set; }
    public string? CompanyName { get; set; }
    public string? Cnpj { get; set; }
    public bool IsAtiva { get; set; }

    public ICollection<UserEntity> Users { get; set; }
    public ICollection<AssetEntity> Asset { get; set; }
    public ICollection<LoanAssetEntity> Loans { get; set; }

}