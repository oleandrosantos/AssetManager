using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Domain.Entities;

public class UserEntity
{
    public string IdUser { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int IdCompany { get; set; }
    public bool isActive { get; set; }
    public AssetEventsEntity Company { get; set; }
    public ICollection<LoanAssetEntity> Loans { get; set; }
}