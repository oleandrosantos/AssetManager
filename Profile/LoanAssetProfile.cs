using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Profile
{
    public class LoanAssetProfile : AutoMapper.Profile
    {
        public LoanAssetProfile()
        {
            CreateMap<CreateLoanAsset, LoanAssetModel>();
        }
    }
}
