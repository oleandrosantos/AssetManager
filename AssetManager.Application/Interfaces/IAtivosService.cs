using AssetManager.Application.DTO.Ativo;

namespace AssetManager.Application.Interfaces;
public interface IAtivosService
{
    Task CriarAtivo(AtivoDTO ativo);
    Task<IList<AtivoDTO>> ObterTodosAtivosDaCompanhia(int idCompanhia);
    Task<AtivoDTO?> ObterAtivoPorID(int idAtivo);
    Task AtualizarAtivo(AtualizarAtivoDTO ativo);
    Task DeletarAtivo(int idAtivo, string informacoesDaExclusao);
}