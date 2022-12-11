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
    private CompanyRepository _companyRepository;

    public AssetController(AssetRepository assetRepository, IMapper mapper, CompanyRepository companyRepository)
    {
        _assetRepository = assetRepository;
        _mapper = mapper;
        _companyRepository = companyRepository;
    }

    [HttpPost("Create")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Create(CreateAsset asset)
    {
        AssetModel? assetModel = _mapper.Map<CreateAsset, AssetModel>(asset);
        assetModel.Company = _companyRepository.GetCompanyByID(asset.IdCompany);
        assetModel = _assetRepository.Create(assetModel);
        
        if (assetModel != null)
            return Ok($"{assetModel.AssetName} cadastrado com sucesso");

        return BadRequest("Erro, não foi possivel criar o Asset");
    }
    
    [HttpPatch("Update/{idAsset}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Update(int idAsset, CreateAsset asset)
    {
        AssetModel? assetModel = _mapper.Map<CreateAsset, AssetModel>(asset, _assetRepository.GetAssetByID(idAsset));
        assetModel.IdAsset = idAsset;
        assetModel = _assetRepository.Update(assetModel);
        if (assetModel != null)
        {
            return Ok($"{assetModel.AssetName} Atualizado com sucesso!");
        }

        return BadRequest("Erro, não foi possivel criar o Asset");
        
    }
    
    [HttpGet("AssetCompanyList/{idCompany}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult AssetCompanyList(int idCompany)
    {
        var assetList = _assetRepository.AssetCompanyList(idCompany);

        if (assetList.Result.Count == 0)
            return NoContent();

        return Ok(assetList.Result);
    }

    [HttpGet("Asset/{id}")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult GetAssetByID(int id)
    {
        AssetModel? asset = _assetRepository.GetAssetByID(id);
        if (asset == null)
            return NoContent();

        return Ok(asset);
    }

    [HttpPut("DeleteAsset/{idAsset}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult DeleteAsset(int idAsset, [FromBody]string exclusionInfo)
    {
        if (_assetRepository.DeleteAsset(idAsset, exclusionInfo))
        {
            return Ok("O ativo foi excluido");
        }
        return BadRequest("Não conseguimos deletar o ativo");
    }


}