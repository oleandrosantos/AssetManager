using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AssetManager.Model;
using Microsoft.IdentityModel.Tokens;

namespace AssetManager.Service;

public class TokenService
{
    private static IConfiguration _configuration;

    public TokenService(IConfiguration cofiguration)
    {
        _configuration = cofiguration;
    }
    public static string GenerateToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = _configuration.GetSection("DataConfigure").GetSection("keyJwt");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Role, user.role),
            }),
            Expires = DateTime.UtcNow.AddHours(24),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}