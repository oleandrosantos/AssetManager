using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Interfaces;

public interface IUserService
{
    public string Create(CreateUserViewModel newUser);
    public Result Login(string email, string password);
    public UserModel? BuscarPorEmail(string email);
}