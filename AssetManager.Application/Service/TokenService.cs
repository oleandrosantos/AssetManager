using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AssetManager.Application.DTO.Token;
using AssetManager.Application.DTO.Usuario;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AssetManager.Application.Service;

public class TokenService: ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GerarToken2(UsuarioDTO usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role),
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
    public string GerarToken(UsuarioDTO usuario)
    {
        var claims = ObterClaimsDoUsuario(usuario);
        return CreateToken(claims);
    }

    public string GerarToken(IEnumerable<Claim> claims)
    {
        return CreateToken(claims);
    }

    public string GerarRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public DateTime ObterDataExpiracaoRefreshToken() => DateTime.UtcNow.AddMilliseconds(int.Parse(_configuration["JWT:ValidadeRefreshTokenEmMinutos"]));


    public TokenModel AuthenticarAtravesDoRefreshToken(TokenModel token)
    {
        var claims = ObterClaimsDeTokenExpirado(token.AcessToken);
        var novoAcessToken = CreateToken(claims);
        return new TokenModel()
        {
            AcessToken = novoAcessToken,
            RefreshToken = GerarRefreshToken()
        };
    }

    public IEnumerable<Claim> ObterClaimsDeTokenExpirado(string? token)
    {
        try
        {
        var tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"])),
            ValidateLifetime = false,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
        };

        var tokenHandler = new JwtSecurityTokenHandler();


        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                        out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                      !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                     StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

        return principal.Claims;
        }
        catch (Exception ex)
        {
            var exemptio = ex;
            return Enumerable.Empty<Claim>();
        }
    }

    private string CreateToken(IEnumerable<Claim> authClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(authClaims),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }

    private IEnumerable<Claim> ObterClaimsDoUsuario(UsuarioDTO usuario)
    {
        return new Claim[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role),
            };
    }
}