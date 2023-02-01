using AssetManager.Application.DTO.User;

namespace AssetManager.Application.Interfaces;
public interface IUserService
{
  public Task<string> Create(CreateUserDTO newUser);
  public Task<UserDTO> Login(string email, string password);
  public Task<UserDTO?> BuscarPorEmail(string email);
  public Task<bool> UpdateUser(UpdateUserDTO dadosDoUsuario);
}