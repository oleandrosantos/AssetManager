using AssetManager;
using AssetManager.Model;
using AssetManager.Repository;

namespace AssetManager.Service;

public class AssetService :IAssetService
{
    private AssetRepository _assetRepository;
    public AssetService(AssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }
    public string Create (AssetModel asset)
    {
        var result = _assetRepository.Create(asset);
        if (result == null)
        {
            throw new Exception("Não foi possivel cadastrar o Asset no banco");
        }
        return $"{result.assetName} foi cadastrado com sucesso sob o id = {result.idAsset}";
    }

    public string Update(AssetModel asset)
    {
        var result = _assetRepository.Update(asset);
        if (result == null)
        {
            throw new Exception("Não foi possivel Atualizado o Asset no banco");
        }
        return $"{result.assetName} foi Atualizado com sucesso sob o id = {result.idAsset}";
    }
}