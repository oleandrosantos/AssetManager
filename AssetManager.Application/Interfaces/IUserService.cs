using AssetManager.Domain.Utils;
using AssetManager.Infra.Data.DTO.User;

namespace AssetManager.Application.Interfaces;
public interface IUserService
{
  public string Create(CreateUserDTO newUser);
  public ResultRequest Login(string email, string password);
  public UserDTO? BuscarPorEmail(string email);
  public bool UpdateUser(UpdateUserDTO dadosDoUsuario);
}