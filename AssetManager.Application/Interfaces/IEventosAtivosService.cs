using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Domain.Entities;

namespace AssetManager.Application.Interfaces;
public interface IEventosAtivosService
{
    Task EncerrarEmprestimoDoAtivo(FimEmprestimoAtivoDTO dadosFimEmprestimo);
    Task<EventosAtivoDTO?> ObterEventosPorId(int id);
    Task<IList<EventosAtivoDTO>?> ObterTodosOsEventosDoAtivo(int idAtivo);
    Task<IList<EventosAtivoDTO>?> ObterTodosOsEventosDosAtivosDaCompanhia(int idCompanhia);
    Task<IList<EventosAtivoDTO>?> ObterTodosOsEventosDosAtivosDoUsuario(string idUsuario);
    Task EmprestarAtivo(EmpretimoAtivoDTO emprestimoATivo);
}