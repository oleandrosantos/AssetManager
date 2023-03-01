using AssetManager.Application.DTO.Ativo;
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
public class AtivosController : Controller
{
    private readonly IAtivosService _ativosService;
    private readonly IEventosAtivosService _eventosAtivoService;

    public AtivosController(IAtivosService ativosService, IEventosAtivosService assetEventsService)
    {
        _ativosService = ativosService;
        _eventosAtivoService = assetEventsService;
    }

    [HttpPost("Create")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Create(AtivoDTO asset)
    {
        try
        {
            var result = _ativosService.CriarAtivo(asset);

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
    public IActionResult Update(AtualizarAtivoDTO asset)
    {
        try
        {
            var result = _ativosService.AtualizarAtivo(asset);

            return Ok("Atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("AssetCompanyList/{IdCompanhia}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult AssetCompanyList(int IdCompanhia)
    {
        var assetList = _ativosService.ObterTodosAtivosDaCompanhia(IdCompanhia).Result;

        if (assetList.Count == 0)
            return NoContent();

        return Ok(assetList);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult GetAssetByID(int id)
    {
        AtivoDTO? asset = _ativosService.ObterAtivoPorID(id).Result;
        bool loanable = asset.IsLoanable();
        if (asset == null)
            return NoContent();

        return Ok(asset);
    }


    [HttpGet("GetAssetEvents/{id}")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult GetAssetEvents(int id)
    {
        EventosAtivoDTO? asset = _eventosAtivoService.ObterEventosPorId(id).Result;
        if (asset == null)
            return NoContent();

        return Ok(asset);
    }

    [HttpDelete("DeleteAsset/{idAtivo}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult DeleteAsset(int idAtivo, [FromBody] string exclusionInfo)
    {
        try
        {
            if (_ativosService.DeletarAtivo(idAtivo, exclusionInfo).IsCompletedSuccessfully)
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
    public IActionResult LoanAsset(EmpretimoAtivoDTO loanAsset)
    {
        try
        {
            var resultado = _eventosAtivoService.EmprestarAtivo(loanAsset);

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
    public IActionResult TerminationLoanAsset(FimEmprestimoAtivoDTO terminationLoanAsset)
    {
        try
        {
            var result = _eventosAtivoService.EncerrarEmprestimoAtivo(terminationLoanAsset);
               
            return Ok("Contrato encerrado com sucesso!");

        }
        catch (Exception ex)
        {
            return BadRequest("Houve um Erro");
        }
    }

    [HttpGet("ListarPorCompanhia/{IdCompanhia}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult CompanyLoanAssetsList(int IdCompanhia)
    {
        try
        {
            var loanList = _eventosAtivoService.ObterTodosOsEventosDosAtivosDaCompanhia(IdCompanhia);

            if (loanList.IsCompleted && loanList.Result.Any())
                return Ok(loanList.Result);

            throw new ObjetoVazioException("");
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
        var loanList = _eventosAtivoService.ObterTodosOsEventosDosAtivosDoUsuario(idUsuario);

        if (loanList.Result.Any())
            return Ok(loanList.Result);

        return BadRequest();
    }


}