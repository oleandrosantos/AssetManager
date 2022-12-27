using AssetManager.Domain.DTO;
using AssetManager.Domain.Utils;

namespace AssetManager.Domain.Interfaces.Application;

public interface IUserService
{
  public string Create(CreateUserDTO newUser);
  public ResultRequest Login(string email, string password);
  public UserDTO? BuscarPorEmail(string email);
  public bool UpdateUser(UpdateUserDTO dadosDoUsuario);
}