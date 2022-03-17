using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager;

public interface IUserService
{
    public string Create(CreateUserViewModel newUser);
}