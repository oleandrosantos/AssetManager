using AssetManager.Application.DTO.Token;
using AssetManager.Application.DTO.Usuario;
using System.Security.Claims;

namespace AssetManager.Application.Interfaces;
public interface ITokenService
{
    TokenModel AuthenticarAtravesDoRefreshToken(TokenModel token);
    string GerarRefreshToken();
    public string GerarToken(UsuarioDTO usuario);
    public string GerarToken(IEnumerable<Claim> claims);
    IEnumerable<Claim> ObterClaimsDeTokenExpirado(string? token);
    DateTime ObterDataExpiracaoRefreshToken();
}
