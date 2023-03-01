using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface IEventosAtivosRepository : IRepositoryBase<EventosAtivoEntity>
{
    Task<IList<EventosAtivoEntity>> ObterTodosOsEventosPorIdAtivo(int idAtivo);
    Task<EventosAtivoEntity> ObterPorId(int id);
    Task<IList<EventosAtivoEntity>?> ObterTodosOsEventosDoAtivo(int idAtivo);
    Task<IList<EventosAtivoEntity>?> ObterTodosOsEventosDosAtivosDaCompanhia(int idCompanhia);
    Task<IList<EventosAtivoEntity>?> ObterTodosOsEventosDosAtivosDoUsuario(string idUsuario);
    Task EmprestarAtivo(EventosAtivoEntity eventoAtivo);
}