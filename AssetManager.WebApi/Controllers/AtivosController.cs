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

    [HttpPost("Cadastrar")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Cadastrar(AtivoDTO ativo)
    {
        try
        {
            var resultado = _ativosService.CriarAtivo(ativo);

            if (resultado.IsCompleted)
                return Ok("Cadastrado com Sucesso");
            else
                throw new Exception();

        }
        catch (Exception)
        {
            return BadRequest("Nâo foi possivel cadastrar");
        }
    }

    [HttpPatch("Atualizar")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult Atualizar(AtualizarAtivoDTO ativo)
    {
        try
        {
            var resultado = _ativosService.AtualizarAtivo(ativo);

            return Ok("Atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ListarAtivosDaCompanhia/{IdCompanhia}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult ListarAtivosDaCompanhia(int idCompanhia)
    {
        var listaAtivos = _ativosService.ObterTodosAtivosDaCompanhia(idCompanhia).Result;

        if (listaAtivos.Count == 0)
            return NoContent();

        return Ok(listaAtivos);
    }

    [HttpGet("ObterAtivoPorID/{id}")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult ObterAtivoPorID(int idAtivo)
    {
        AtivoDTO? ativo = _ativosService.ObterAtivoPorID(idAtivo).Result;
        bool eLocavel = ativo.ELocavel();
        if (ativo == null)
            return NoContent();

        return Ok(ativo);
    }


    [HttpGet("ObterEventosPorID/{idEvento}")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult ObterEventosPorID(int idEvento)
    {
        EventosAtivoDTO? ativo = _eventosAtivoService.ObterEventosPorId(idEvento).Result;
        if (ativo == null)
            return NoContent();

        return Ok(ativo);
    }

    [HttpDelete("DeletarAtivo/{idAtivo}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult DeletarAtivo(int idAtivo, [FromBody] string dadosExclusaoAtivo)
    {
        try
        {
            if (_ativosService.DeletarAtivo(idAtivo, dadosExclusaoAtivo).IsCompletedSuccessfully)
                return Ok("O ativo foi excluido");

            throw new Exception();
        }
        catch(Exception)
        {
            return BadRequest("Não conseguimos deletar o ativo");
        }

    }

    [HttpPost("EmprestarAtivo")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult EmprestarAtivo(EmpretimoAtivoDTO dadosEmprestimoAtivo)
    {
        try
        {
            var resultado = _eventosAtivoService.EmprestarAtivo(dadosEmprestimoAtivo);

            if (resultado.IsFaulted)
                throw resultado.Exception;

            return Ok("Contrato registrado com sucesso");
        }
        catch(Exception e)
        {
            return BadRequest("Houve um Erro");
        }
    }


    [HttpPut("EncerrarEmprestimoDoAtivo")]
    [Authorize(Roles = "Administrador, Suporte")]
    public IActionResult EncerrarEmprestimoDoAtivo(FimEmprestimoAtivoDTO dadosFimEmprestimoAtivo)
    {
        try
        {
            var resultado = _eventosAtivoService.EncerrarEmprestimoDoAtivo(dadosFimEmprestimoAtivo);
               
            return Ok("Contrato encerrado com sucesso!");

        }
        catch (Exception ex)
        {
            return BadRequest("Houve um Erro");
        }
    }

    [HttpGet("ListarAtivosPorCompanhia/{idCompanhia}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult ListarAtivosPorCompanhia(int idCompanhia)
    {
        try
        {
            var loanList = _eventosAtivoService.ObterTodosOsEventosDosAtivosDaCompanhia(idCompanhia);

            if (loanList.IsCompleted && loanList.Result.Any())
                return Ok(loanList.Result);

            throw new ObjetoVazioException("");
        }
        catch(Exception ex)
        {
            return NoContent();
        }
    }

    [HttpGet("ObterAtivosPorUsuario/{idUsuario}")]
    [Authorize(Roles = "Administrador, Suporte, Funcionario")]
    public IActionResult ObterAtivosPorUsuario(string idUsuario)
    {
        var loanList = _eventosAtivoService.ObterTodosOsEventosDosAtivosDoUsuario(idUsuario);

        if (loanList.Result.Any())
            return Ok(loanList.Result);

        return BadRequest();
    }


}