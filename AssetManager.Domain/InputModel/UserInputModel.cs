using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AssetManager.Domain.InputModel
{
    public class UserInputModel
    {
        [MaxLength(32)]
        public string IdUsuario { get; set; }
        [Required(ErrorMessage = "Nome nao informado")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email nao informado")]
        [MaxLength(256)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha nao informada")]
        public string Password { get; set; }
        public string Role { get; set; }
        public int IdCompany { get; set; }
    }
}
