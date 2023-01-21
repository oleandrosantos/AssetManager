using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AssetManager.Domain.DTO;
using AssetManager.Domain.Interfaces.Application;
using Microsoft.IdentityModel.Tokens;

namespace AssetManager.Application.Service;

public class TokenService: ITokenService
{
    private readonly AppSettings _appSettings;
    public TokenService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
    public string GenerateToken(UserDTO user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}