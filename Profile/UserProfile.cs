
using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Profile;

public class UserProfile :AutoMapper.Profile
{
    
    public UserProfile()
    {
        CreateMap<CreateUserViewModel, UserModel>();

        CreateMap<UserModel, CreateUserViewModel>();
    }
    
}