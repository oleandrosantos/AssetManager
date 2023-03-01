using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.Usuario;
public class AutenticarUsuario
{
    [Required]
    [MaxLength(256,ErrorMessage = "O Email deve ter no maximo 256 caracteres")]
    [MinLength(10, ErrorMessage = "O Email deve ter no minimo 10 caracteres")]
    public string Email { get; set; }
    [Required]
    [MaxLength(128, ErrorMessage = "O Email deve ter no maximo 128 caracteres")]
    [MinLength(8, ErrorMessage = "O Email deve ter no minimo 8 caracteres")]
    public string Password { get; set; }

}