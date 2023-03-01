using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.Usuario;
public class UsuarioDTO
{
    public string IdUsuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int IdCompanhia { get; set; }
}
