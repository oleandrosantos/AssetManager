using AssetManager.Application.DTO.User;

namespace AssetManager.Application.Interfaces;
public interface IUserService
{
  public Task Create(CreateUserDTO newUser);
  public Task<UserDTO> Login(string email, string password);
  public Task<UserDTO?> BuscarPorEmail(string email);
  public Task UpdateUser(UpdateUserDTO dadosDoUsuario);
}