using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Domain.Entities;

[Table("tb_loanasset")]
public class LoanAssetEntity
{
    public string? IdLoanAsset { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? DevolutionDate { get; set; }
    public string? Description { get; set; }    
    public string IdUser { get; set; }    
    public int IdAsset { get; set; }    
    public int IdCompany { get; set; }
    public UserEntity User { get; set; }
    public AssetEntity Asset { get; set; }
    public CompanyEntity Company { get; set; }
}