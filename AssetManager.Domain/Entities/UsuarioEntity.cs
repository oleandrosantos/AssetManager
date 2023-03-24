using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Domain.Entities;

public class UsuarioEntity
{
    public string IdUsuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int IdCompanhia { get; set; }
    public bool Ativo { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? DataExpiracaoRefreshToken { get; set; }
    public CompanhiaEntity Companhia { get; set; }
    public ICollection<EventosAtivoEntity> EventosAtivo { get; set; }


    public bool TokenExpirado()
    {
        if (DataExpiracaoRefreshToken == null)
            return false;

        return DataExpiracaoRefreshToken <= DateTime.Now;
    }
}