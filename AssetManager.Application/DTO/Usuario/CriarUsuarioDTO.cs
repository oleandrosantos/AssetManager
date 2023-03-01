using AssetManager.Application.Helpers;
using AssetManager.Domain.Validations;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.Usuario;
public class CriarUsuarioDTO
{
    private string _password {get; set;}

    [Required(ErrorMessage = "Nome não informado")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Senha não informada")]
    public string Password { 
        get => _password; 
        set => _password = PasswordHelper.CriandoHashDaSenha(value);
    }

    [Required(ErrorMessage = "Email não informado")]
    [MaxLength(256)]
    public string Email { get; set; }
    public string? Role { get; set; }
    public int IdCompanhia { get; set; }


    public void Validate()
    {
        DominioInvalidoException.When(string.IsNullOrEmpty(Nome),"Nome não informado");
        DominioInvalidoException.When(string.IsNullOrEmpty(Password), "Senha não informada");
        DominioInvalidoException.When(string.IsNullOrEmpty(Email), "Email não informada");
        DominioInvalidoException.When(IdCompanhia == null || IdCompanhia == 0, "Email não informada");
    }
}
