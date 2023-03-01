using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys
{
    public interface IAtivosRepository : IRepositoryBase<AtivoEntity>
    {
        Task<AtivoEntity?> ObterAtivoPorSKU(string SKU);
        Task<IList<AtivoEntity>> ObterAtivosDaCompanhia(int IdCompanhia);
        Task Delete(int id, string? ExclusionInfo);
        Task<AtivoEntity?> ObterAtivoPorId(int id);
    }
}
