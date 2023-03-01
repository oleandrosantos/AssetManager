using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;

namespace AssetManager.Infra.Data.Repository;
public class EventosAtivoRepository : RepositoryBase<EventosAtivoEntity>, IEventosAtivosRepository
{
	public EventosAtivoRepository(DataContext dbContext) : base(dbContext) { }

    public Task<EventosAtivoEntity?> ObterPorId(int id)
    {
        var eventoAtivo = dbSet.Where(a => a.IdEventosAtivo == id).FirstOrDefault();
        return Task.FromResult(eventoAtivo);
    }
    public Task EmprestarAtivo(EventosAtivoEntity eventoAtivo)
    {
        context.assetEvents.Add(eventoAtivo);
        context.SaveChanges();
        return Task.FromResult(eventoAtivo.IdEventosAtivo);
    }

    public async Task<IList<EventosAtivoEntity>> ObterTodosOsEventosPorIdAtivo(int idAtivo)
    {
        var events = dbSet
            .Where(a => a.IdAtivo == idAtivo)
            .OrderBy(a => a.DataEvento)
            .ToList();

        return events;
    }

    public async Task<IList<EventosAtivoEntity>?> ObterTodosOsEventosDoAtivo(int idAtivo)
    {
        var AssetList = dbSet.Where(a => a.Ativo.IdAtivo == idAtivo).ToList();
        return AssetList;
    }

    public async Task<IList<EventosAtivoEntity>?> ObterTodosOsEventosDosAtivosDaCompanhia(int IdCompanhia)
    {
        var AssetList = dbSet.Where(a => a.Ativo.Companhia.IdCompanhia == IdCompanhia).ToList();
        return AssetList;
    }

    public async Task<IList<EventosAtivoEntity>?> ObterTodosOsEventosDosAtivosDoUsuario(string IdUsuario)
    {
        var AssetList = dbSet.Where(a => a.Usuario.IdUsuario == IdUsuario).ToList();
        return AssetList;
    }
}
