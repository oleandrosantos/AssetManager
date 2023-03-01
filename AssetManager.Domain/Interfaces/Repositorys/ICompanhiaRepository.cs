using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface ICompanhiaRepository : IRepositoryBase<CompanhiaEntity>
{
    Task<CompanhiaEntity?> ObterCompanhiaPorId(int id);
    Task Delete(int id);
    Task<IList<CompanhiaEntity>> ObterTodasAsCompanhias();
}