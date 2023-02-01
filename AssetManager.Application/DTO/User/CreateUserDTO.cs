using AssetManager.Application.Helpers;

namespace AssetManager.Application.DTO.User;
public class CreateUserDTO
{
    private string _password {get; set;}
    public string? Name { get; set; }
    public string Password { 
        get => _password; 
        set => PasswordHelper.CriandoHashDaSenha(value);
    }
    public string Email { get; set; }
    public string? Role { get; set; }
    public int IdCompany { get; set; }
}
