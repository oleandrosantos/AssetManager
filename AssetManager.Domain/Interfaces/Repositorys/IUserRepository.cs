using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface IUserRepository : IRepositoryBase<UserEntity>
{
    UserEntity? GetUserByEmail(string email);
}