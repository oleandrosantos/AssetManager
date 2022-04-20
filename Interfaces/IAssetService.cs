using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Interfaces;
public interface IAssetService
{
    public string Create(CreateAsset asset);

    public string Update(AssetModel asset);

}