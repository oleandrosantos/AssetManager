
using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Profile;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserViewModel, UserModel>()
                .AfterMap((src, dest) =>
                {
                    dest.isActive = true;
                });

        CreateMap<UpdateUserViewModel, UserModel>();
    }

}