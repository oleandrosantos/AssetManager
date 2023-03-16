using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings").GetSection("Secret").Value);
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
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = ObterClaimsDoUsuario(usuario);
        var tokenCreated = CreateToken(claims.ToArray());
        var token = tokenHandler.CreateToken(tokenCreated);
        return tokenHandler.WriteToken(token);
    }

    public string GerarToken(Claim[] claims)
    {
        var token = new JwtSecurityTokenHandler().CreateToken(CreateToken(claims));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GerarRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public DateTime ObterDataExpiracaoRefreshToken() => DateTime.UtcNow.AddMilliseconds(int.Parse(_configuration["JWT:ValidadeRefreshTokenEmMinutos"]));

    public ClaimsPrincipal? ObterClaimsDeTokenExpirado(string? token)
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
                  !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                                 StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    private SecurityTokenDescriptor? CreateToken(Claim[] authClaims)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings").GetSection("Secret").Value);
        _ = int.TryParse(_configuration["JWT:ValidadeAcessTokenEmMinutos"], out
            int tokenValidityInMinutes);

        var token = new SecurityTokenDescriptor{
            Expires = DateTime.Now.AddMinutes(tokenValidityInMinutes),
            Subject = new ClaimsIdentity(authClaims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return token;
    }

    private List<Claim> ObterClaimsDoUsuario(UsuarioDTO usuario)
    {
        return new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role),
            };
    }
}