using AssetManager.Model;
using AssetManager.Repository;

namespace AssetManager.Service;

public class AssetService
{
    private AssetRepository _assetRepository;
    public AssetService(AssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }
    public string createAsset(AssetModel asset)
    {
        var result = _assetRepository.Create(asset);
        return result;
    }
}