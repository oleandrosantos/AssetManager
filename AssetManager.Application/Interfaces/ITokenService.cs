using AssetManager.Application.DTO.User;

namespace AssetManager.Application.Interfaces;
public interface ITokenService
{
    public string GenerateToken(UserDTO user);
}
