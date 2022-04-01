using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Profile
{
    public class AssetProfile :AutoMapper.Profile
    {
        public AssetProfile()
        {
            CreateMap<AssetModel, CreateAsset>();

            CreateMap<CreateAsset, AssetModel>();
        }
    }
}
