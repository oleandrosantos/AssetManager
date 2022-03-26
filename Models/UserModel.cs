using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Model;

[Table("tb_usuario")]
public class UserModel
{
    [Key]
    [MaxLength(32)]
    public string idUsuario { get; set; }
    [Required(ErrorMessage = "Nome nao informado")]
    public string name { get; set; }
    [Required(ErrorMessage = "Email nao informado")]
    [MaxLength(256)]
    public string email { get; set; }
    [Required(ErrorMessage = "Senha nao informada")]
    public string password { get; set; }
    public string token { get; set; }
    public string role { get; set; }
    public CompanyModel company { get; set; }
}