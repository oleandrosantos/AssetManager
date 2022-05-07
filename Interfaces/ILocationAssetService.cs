using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Interfaces
{
    public interface ILocationAssetService
    {
        public Result CreateLocationAsset(CreateLocationAsset locationAsset);
        public Result DeleteLocationAsset(int id);
        public Result UpdateLocationAsset(LocationAssetModel locationAssetModel);
        public List<LocationAssetModel> CompanyLocationAssetsList(int idCompany);
        public List<LocationAssetModel> UserAssetLocationList(string idUser);
    }
}
