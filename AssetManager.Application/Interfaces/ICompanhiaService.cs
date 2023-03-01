using AssetManager.Application.DTO.Companhia;

namespace AssetManager.Application.Interfaces;
public interface ICompanhiaService
{
    Task CriarCompanhia(CriarCompanhiaDTO companhia);
    Task AtualizarCompanhia(CompanhiaDTO companhiaDTO);
    Task<CompanhiaDTO?> ObterCompanhiaPorId(int id);
    Task DeletarCompanhia(int idCompanhia);
    Task<List<CompanhiaDTO>> ObterTodasAsCompanhias();
}
