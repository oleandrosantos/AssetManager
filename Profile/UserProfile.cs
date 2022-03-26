
using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Profile;

public class UserProfile :AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<UserViewModel, UserModel>()
            .ForMember(dest => dest.company, c=> 
                c.MapFrom(co => co.company.idCompany));
        
        CreateMap<UserModel,UserViewModel>()
            .ForMember(dest => dest.company.idCompany, c =>
                c.MapFrom(co => co.company.idCompany));
    }
    
}