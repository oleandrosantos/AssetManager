using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Profile
{
    public class AssetProfile :AutoMapper.Profile
    {
        public AssetProfile()
        {
            CreateMap<AssetModel, AssetProfile>();

            CreateMap<AssetProfile, AssetModel>();
        }
    }
}
