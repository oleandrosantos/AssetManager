using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetController : ControllerBase
{
    private IAssetService _assetService;
    public AssetController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpPost("Create")]
    public IActionResult Create(CreateAsset asset)
    {
        var resultado = _assetService.Create(asset);
        if (resultado != null)
        {
            return Ok(resultado);
        }

        return BadRequest("Erro, não foi possivel criar o Asset");
    }
    
    [HttpPatch("Update")]
    public IActionResult Update(CreateAsset asset)
    {   
        var resultado = _assetService.Update(asset);
        if (resultado != null)
        {
            return Ok(resultado);
        }

        return BadRequest("Erro, não foi possivel criar o Asset");
        
    }
    [HttpGet("AssetCompanyList/{idCompany}")]
    public IActionResult AssetCompanyList(int idCompany)
    {
        var assetList = _assetService.AssetCompanyList(idCompany);
        if(assetList != null)
        {
            return Ok(assetList);
        }
        return NoContent();
    }
    [HttpPut("DeleteAsset")]
    public IActionResult DeleteAsset(int idAsset, string exclusionInfo)
    {
        if(_assetService.DeleteAsset(idAsset, exclusionInfo))
        {
            return Ok("O ativo foi excluido");
        }
        return BadRequest("Não conseguimos deletar o ativo");
    }
}