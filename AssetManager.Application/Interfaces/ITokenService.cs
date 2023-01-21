using AssetManager.Infra.Data.DTO.User;

namespace AssetManager.Application.Interfaces;
public interface ITokenService
{
    public string GenerateToken(UserDTO user);
}
