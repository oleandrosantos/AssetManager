using AssetManager.Model;

namespace AssetManager.Interfaces;
public interface IAssetService
{
    public string Create(AssetModel asset);

    public string Update(AssetModel asset);

}