using AssetManager.Model;

namespace AssetManager.Profile
{
    public class LocationAssetProfile : AutoMapper.Profile
    {
        public LocationAssetProfile()
        {
            CreateMap<LocationAssetProfile, LocationAssetModel>();

            CreateMap<LocationAssetModel, LocationAssetProfile>();
        }
    }
}
