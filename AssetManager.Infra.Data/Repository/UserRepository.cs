using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
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
            return context.usuario.FirstOrDefault(u => u.Email == email);
        }
    }
}
