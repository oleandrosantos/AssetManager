using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.User;
public class UserDTO
{
    public string IdUsuario { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int IdCompany { get; set; }
}
