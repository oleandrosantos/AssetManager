using AssetManager.Application.DTO.Asset;
using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Domain.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetController : Controller
{
    private readonly IAssetService _assetService;
    private readonly IAssetEventsService _assetEventService;

    public AssetController(IAssetService assetService, IAssetEventsService assetEventsService)
    {
        _assetService = assetService;
        _assetEventService = assetEventsService;
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

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult GetAssetByID(int id)
    {
        AssetDTO? asset = _assetService.GetByID(id).Result;
        bool loanable = asset.IsLoanable();
        if (asset == null)
            return NoContent();

        return Ok(asset);
    }


    [HttpGet("GetAssetEvents/{id}")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult GetAssetEvents(int id)
    {
        AssetEventsDTO? asset = _assetEventService.GetAssetEventsById(id).Result;
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

    [HttpPost("LoanAsset")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult LoanAsset(LoanAssetDTO loanAsset)
    {
        try
        {
            var resultado = _assetEventService.LoanAsset(loanAsset);

            if (resultado.IsFaulted)
                throw resultado.Exception;

            return Ok("Contrato registrado com sucesso");
        }
        catch(Exception e)
        {
            return BadRequest("Houve um Erro");
        }
    }


    [HttpPut("EncerrandoContrato")]
    [Authorize(Roles = "Administrador, Suporte")]
    public IActionResult TerminationLoanAsset(TerminationLoanAssetModel terminationLoanAsset)
    {
        try
        {
            var result = _assetEventService.TerminateLoanContract(terminationLoanAsset);
               
            return Ok("Contrato encerrado com sucesso!");

        }
        catch (Exception ex)
        {
            return BadRequest("Houve um Erro");
        }
    }

    [HttpGet("ListarPorCompanhia/{idCompany}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult CompanyLoanAssetsList(int idCompany)
    {
        try
        {
            var loanList = _assetEventService.GetLoanAssetsByCompany(idCompany);

            if (loanList.IsCompleted && loanList.Result.Any())
                return Ok(loanList.Result);

            throw new EmptyReturnException("");
        }
        catch(Exception ex)
        {
            return NoContent();
        }
    }

    [HttpGet("ListarPorUsuario/{idUsuario}")]
    [Authorize(Roles = "Administrador, Suporte, Funcionario")]
    public IActionResult UserAssetLoanList(string idUsuario)
    {
        var loanList = _assetEventService.GetLoanAssetsByUser(idUsuario);

        if (loanList.Result.Any())
            return Ok(loanList.Result);

        return BadRequest();
    }


}