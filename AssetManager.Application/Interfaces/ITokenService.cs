using AssetManager.Application.DTO.Usuario;

namespace AssetManager.Application.Interfaces;
public interface ITokenService
{
    public string GerarToken(UsuarioDTO usuario);
}
