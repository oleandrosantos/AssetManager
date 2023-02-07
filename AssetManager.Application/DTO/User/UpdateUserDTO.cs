using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AssetManager.Application.Helpers;

namespace AssetManager.Application.DTO.User;
public class UpdateUserDTO
{
    private string? _password { get; set; }
    public string? Name { get; set; }
    public string? Password
    {
        get => _password;
        set => PasswordHelper.CriandoHashDaSenha(value);
    }
    [Required]
    public string Email { get; set; }
    public string? Role { get; set; }
    [DefaultValue(true)]
    public bool? IsActive { get; set; }
}
