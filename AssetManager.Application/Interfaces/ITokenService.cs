using AssetManager.Application.DTO.Usuario;
using System.Security.Claims;

namespace AssetManager.Application.Interfaces;
public interface ITokenService
{
    string GerarRefreshToken();
    public string GerarToken(UsuarioDTO usuario);
    public string GerarToken(Claim[] claims);
    ClaimsPrincipal? ObterClaimsDeTokenExpirado(string? token);
    DateTime ObterDataExpiracaoRefreshToken();
}
