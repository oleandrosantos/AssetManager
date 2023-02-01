using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Domain.Validations;
using AssetManager.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.Repository
{
    public class UserRepository: RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(DataContext DbContext):base(DbContext) { }

        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            var user = context.usuario.FirstOrDefault(u => u.Email == email);
            
            if (user == null)
                throw new EmptyReturnException("Não existe usuario cadastrado neste email");
            
            return user;
        }
    }
}
