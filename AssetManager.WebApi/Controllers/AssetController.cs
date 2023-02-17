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
    private readonly IAssetService _assetService;

    public AssetController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpPost("Create")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Create(AssetDTO asset)
    {
        try
        {
            var result = _assetService.CreateAsset(asset);

            if (result.IsCompleted)
                return Ok("Cadastrado com Sucesso");
            else
                throw new Exception();

        }
        catch (Exception)
        {
            return BadRequest("Nâo foi possivel cadastrar");
        }
    }

    [HttpPatch("Update")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Update(UpdateAssetDTO asset)
    {
        try
        {
            var result = _assetService.UpdateAsset(asset);

            return Ok("Atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
        AssetDTO? asset = _assetService.GetByID(id).Result;
        if (asset == null)
            return NoContent();

        return Ok(asset);
    }

    [HttpDelete("DeleteAsset/{idAsset}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult DeleteAsset(int idAsset, [FromBody] string exclusionInfo)
    {
        try
        {
            if (_assetService.DeleteAsset(idAsset, exclusionInfo).IsCompletedSuccessfully)
                return Ok("O ativo foi excluido");

            throw new Exception();
        }
        catch(Exception)
        {
            return BadRequest("Não conseguimos deletar o ativo");
        }

    }


}