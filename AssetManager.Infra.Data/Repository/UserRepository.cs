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
            var user = context.usuario.FirstOrDefault(u => u.Email == email);
            
            if (user == null)
                EmptyReturnException.When(true, "Não existe usuario cadastrado neste email");
            
            return user;
        }
    }
}
