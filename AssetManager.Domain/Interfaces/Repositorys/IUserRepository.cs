using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface IUserRepository : IRepositoryBase<UserEntity>
{
    Task<UserEntity?> GetUserByEmail(string email);
}