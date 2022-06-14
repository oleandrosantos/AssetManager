using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Model;

[Table("tb_usuario")]
public class UserModel
{
    [Key]
    [MaxLength(32)]
    [Column("IdUsuario")]
    public string IdUsuario { get; set; }
    [Required(ErrorMessage = "Nome nao informado")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email nao informado")]
    [MaxLength(256)]
    public string Email { get; set; }
    [Required(ErrorMessage = "Senha nao informada")]
    public string Password { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    [ForeignKey("Company")]
    [Column("IdCompany")]
    public int IdCompany { get; set; }
    public CompanyModel Company { get; set; }
}