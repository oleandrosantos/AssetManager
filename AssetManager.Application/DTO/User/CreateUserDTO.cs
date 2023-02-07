using AssetManager.Application.Helpers;
using AssetManager.Domain.Validations;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.User;
public class CreateUserDTO
{
    private string _password {get; set;}

    [Required(ErrorMessage = "Nome nao informado")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Senha nao informada")]
    public string Password { 
        get => _password; 
        set => _password = PasswordHelper.CriandoHashDaSenha(value);
    }

    [Required(ErrorMessage = "Email nao informado")]
    [MaxLength(256)]
    public string Email { get; set; }
    public string? Role { get; set; }
    public int IdCompany { get; set; }


    public void Validate()
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(Name),"Nome não informado");
        DomainExceptionValidation.When(string.IsNullOrEmpty(Password), "Senha não informada");
        DomainExceptionValidation.When(string.IsNullOrEmpty(Email), "Email não informada");
        DomainExceptionValidation.When(IdCompany == null || IdCompany == 0, "Email não informada");
    }
}
