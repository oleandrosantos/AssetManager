using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using AssetManager.Application.Interfaces;
using AssetManager.Application.DTO.Companhia;

namespace AssetManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanhiaController : Controller
    {
        private readonly ICompanhiaService _companhiaService;

        public CompanhiaController(ICompanhiaService companhiaService)
        {
            _companhiaService = companhiaService;
        }

        [HttpPost("Cadastrar")]
        [AllowAnonymous]
        public IActionResult Cadastrar(CriarCompanhiaDTO companhiaModel)
        {
            var companhiaResult = _companhiaService.CriarCompanhia(companhiaModel);

            if(companhiaResult.IsCompletedSuccessfully)
                return Ok("Companhia cadastrada com sucesso!");

            return BadRequest($"Não conseguimos cadastrar, verifique os dados!");
        }

        [HttpDelete("DeletarCompanhia/{id}")]
        [Authorize(Roles = "Suporte")]
        public IActionResult DeletarCompanhia(int id)
        {
            try
            {
                _companhiaService.DeletarCompanhia(id);
                return Ok("Companhia Excluida com sucesso!");
            }
            catch(Exception)
            {
                return BadRequest("Não foi possivel Excluir a companhia");
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult Atualizar(CompanhiaDTO companhiaModel)
        {
            try
            {               
                _companhiaService.AtualizarCompanhia(companhiaModel);

                return Ok("Atualizado com sucesso!");
            }
            catch(Exception)
            {
                return BadRequest("Houve um erro e não foi possivel atualizar a companhia");
            }
        }


        [HttpGet("ListarCompanhias")]
        [Authorize(Roles = "Suporte")]
        public IActionResult ListarCompanhias()
        {
            try
            {
                var companhia = _companhiaService.ObterTodasAsCompanhias().Result;
                return Ok(companhia);
            }
            catch (Exception)
            {
                return NoContent();
            }

        }

        [HttpGet("Obtercompanhia/{id}")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public IActionResult ObtercompanhiaPorId(int id)
        {
            try
            {
                var companhia = _companhiaService.ObterCompanhiaPorId(id);
                return Ok(companhia.Result);
            }
            catch(Exception)
            {
                return NoContent();
            }
        }
    }
}
