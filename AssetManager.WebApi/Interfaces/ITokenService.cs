using AssetManager.Model;

namespace AssetManager.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(UserModel user);
    }
}
