using AssetManager.Application.DTO.Usuario;

namespace AssetManager.Application.Interfaces;
public interface IUsuarioService
{
  public Task CadastrarUsuario(CriarUsuarioDTO novoUsuario);
  public Task<string> Login(string email, string password);
  public Task<UsuarioDTO?> BuscarPorEmail(string email);
  public Task AtualizarUsuario(AtualizarUsuarioDTO dadosDoUsuario);
  public Task<List<UsuarioDTO?>> ObterTodosOsUsuarioDaCompanhia(int idCompanhia);
  public Task RevogarAcessoUsuario(string email);
}