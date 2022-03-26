using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager;

public interface IUserService
{
    public string Create(UserViewModel newUser);
    public LoginResult Login(string email, string password);
    public UserModel? BuscarPorEmail(string email);
}