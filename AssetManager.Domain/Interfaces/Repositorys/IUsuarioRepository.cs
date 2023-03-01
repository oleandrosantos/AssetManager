using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface IUsuarioRepository : IRepositoryBase<UsuarioEntity>
{
    Task<UsuarioEntity?> ObterUsuarioPorEmail(string email);
    Task<List<UsuarioEntity?>> ObterUsuariosDaCompanhia(int IdCompanhia);
}