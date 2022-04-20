using AssetManager;
using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.Repository;
using AssetManager.ViewModel;
using AutoMapper;

namespace AssetManager.Service;

public class AssetService :IAssetService
{
    private AssetRepository _assetRepository;
    private IMapper _mapper;
    private ICompanyService _companyService;
    public AssetService(AssetRepository assetRepository, IMapper mapper, ICompanyService companyService)
    {
        _assetRepository = assetRepository;
        _mapper = mapper;
        _companyService = companyService;
        
    }
    public string Create (CreateAsset asset)
    {
        if(asset == null)
        {
            return "Erro";
        }
        AssetModel assetModel = _mapper.Map<CreateAsset, AssetModel>(asset);
        assetModel.company = _companyService.ObterCompanyPorId(asset.idCompany);
        var result = _assetRepository.Create(assetModel);
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