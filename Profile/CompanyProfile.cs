
using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Profile;

public class CompanyProfile : AutoMapper.Profile
{

    public CompanyProfile()
    {
        CreateMap<CreateCompanyViewModel, CompanyModel>();

        CreateMap<CompanyModel, CreateCompanyViewModel>();
    }

}