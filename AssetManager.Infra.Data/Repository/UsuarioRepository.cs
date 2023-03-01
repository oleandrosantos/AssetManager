using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Domain.Validations;
using AssetManager.Infra.Data.Context;

namespace AssetManager.Infra.Data.Repository
{
    public class UsuarioRepository: RepositoryBase<UsuarioEntity>, IUsuarioRepository
    {
        public UsuarioRepository(DataContext DbContext):base(DbContext) { }

        public async Task<UsuarioEntity?> ObterUsuarioPorEmail(string email)
        {
            var usuario = context.usuario.FirstOrDefault(u => u.Email == email);
                        
            return usuario;
        }

        public Task<List<UsuarioEntity?>> ObterUsuariosDaCompanhia(int IdCompanhia)
        {
            var listaUsuario = dbSet.Where(u => u.IdCompanhia == IdCompanhia).ToList();
            return Task.FromResult(listaUsuario);
        }

    }
}
