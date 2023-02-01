using AssetManager.Application.DTO.Asset;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetController : ControllerBase
{
    private IAssetService _assetService;
    private ICompanyService _companyService;

    public AssetController(IAssetService assetService, ICompanyService companyService)
    {
        _assetService = assetService;
        _companyService = companyService;
    }

    [HttpPost("Create")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Create(AssetDTO asset)
    {
        var result = _assetService.CreateAsset(asset);

        if (result.IsCompletedSuccessfully)
            return Ok(result);

        return BadRequest(result);
    }
    
    [HttpPatch("Update/{idAsset}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Update(int idAsset, AssetDTO asset)
    {
        UpdateAssetDTO updateAsset = (UpdateAssetDTO)asset;
        updateAsset.IdAsset = idAsset;
        var result = _assetService.UpdateAsset(updateAsset).Result;

        if (result.IsSucess)
            return Ok(result.Message);

        return BadRequest(result.Message);
    }
    
    [HttpGet("AssetCompanyList/{idCompany}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult AssetCompanyList(int idCompany)
    {
        var assetList = _assetService.GetAssetsByCompany(idCompany).Result;

        if (assetList.Count == 0)
            return NoContent();

        return Ok(assetList);
    }

    [HttpGet("Asset/{id}")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult GetAssetByID(int id)
    {
        AssetDTO? asset = _assetService.GetByID(id);
        if (asset == null)
            return NoContent();

        return Ok(asset);
    }

    [HttpDelete("DeleteAsset/{idAsset}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult DeleteAsset(int idAsset, [FromBody]string exclusionInfo)
    {
        if (_assetService.DeleteAsset(idAsset, exclusionInfo))
            return Ok("O ativo foi excluido");

        return BadRequest("Não conseguimos deletar o ativo");
    }


}