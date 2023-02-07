using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Domain.Validations;
using AssetManager.Infra.Data.Context;

namespace AssetManager.Infra.Data.Repository
{
    public class UserRepository: RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(DataContext DbContext):base(DbContext) { }

        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            var user = Context.usuario.FirstOrDefault(u => u.Email == email);
                        
            return user;
        }
    }
}
