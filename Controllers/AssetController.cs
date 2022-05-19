using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.Repository;
using AssetManager.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetController : ControllerBase
{
    private AssetRepository _assetRepository;
    private IMapper _mapper;
    private CompanyController _companyController;

    public AssetController(AssetRepository assetRepository, IMapper mapper, CompanyController companyController)
    {
        _assetRepository = assetRepository;
        _mapper = mapper;
        _companyController = companyController;
    }

    [HttpPost("Create")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Create(CreateAsset asset)
    {
        AssetModel? assetModel = _mapper.Map<CreateAsset, AssetModel>(asset);
        assetModel.company = _companyController.GetCompanyByID(asset.idCompany).Result.Value;
        assetModel = _assetRepository.Create(assetModel);
        
        if (_assetRepository.Create(assetModel) != null)
            return Ok($"{assetModel.assetName} cadastrado com sucesso");

        return BadRequest("Erro, não foi possivel criar o Asset");
    }
    
    [HttpPatch("Update")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Update(CreateAsset asset)
    {
        AssetModel? assetModel = _assetRepository.Update(_mapper.Map<CreateAsset, AssetModel>(asset));
        if (assetModel != null)
        {
            return Ok($"{assetModel.assetName} Atualizado com sucesso!");
        }

        return BadRequest("Erro, não foi possivel criar o Asset");
        
    }
    [HttpGet("AssetCompanyList/{idCompany}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult AssetCompanyList(int idCompany)
    {
        List<AssetModel>? assetList = _assetRepository.AssetCompanyList(idCompany);

        if (assetList == null || assetList.Count == 0)
        {
            return NoContent();
        }
        return Ok(assetList);
    }
    [HttpPut("DeleteAsset")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult DeleteAsset(int idAsset, string exclusionInfo)
    {
        if (_assetRepository.DeleteAsset(idAsset, exclusionInfo))
        {
            return Ok("O ativo foi excluido");
        }
        return BadRequest("Não conseguimos deletar o ativo");
    }
}