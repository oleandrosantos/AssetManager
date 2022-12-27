using AssetManager.Domain;
using AssetManager.Domain.DTO;

namespace AssetManager.Domain.Interfaces.Application;
{
    public interface ITokenService
    {
        public string GenerateToken(UserDTO user);
    }
}
