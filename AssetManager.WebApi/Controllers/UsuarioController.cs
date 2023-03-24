using AssetManager.Application.DTO.Token;
using AssetManager.Application.DTO.Usuario;
using AssetManager.Application.Enums;
using AssetManager.Application.Interfaces;
using AssetManager.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace AssetManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : Controller
{

    private IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
    _usuarioService = usuarioService;
    }

    [HttpPost("Cadastrar")]
    [AllowAnonymous]
    public IActionResult CadastrarUsuario(CriarUsuarioDTO dadosUsuario)
    {
        try
        {
            dadosUsuario.Email = dadosUsuario.Email.ToLower().Trim();
            var resultado = _usuarioService.CadastrarUsuario(dadosUsuario);

            return Ok("Usuario cadastrado com sucesso!");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public IActionResult Autenticar([FromBody] AutenticarUsuario model)
    {
        try
        {
            model.Email = model.Email.ToLower().Trim();
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return NotFound(new { message = "Email ou senha não informados" });
            }

            var tokenModel = _usuarioService.Login(model.Email, model.Password).Result;
            return Ok(new { tokenModel });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possivel realizar o login, tente novamente" });
        }
    }

    [HttpPut("Update")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult AtualizarUsuario(AtualizarUsuarioDTO dadosUsuario)
    {
        throw new NotImplementedException();
    }

    [HttpPut("RevogarAcesso")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult RevogarAcesso(string email)
    {
        var resultado = _usuarioService.RevogarAcessoUsuario(email);
        if(resultado.IsCompletedSuccessfully)
            return Ok("Acesso do usuario revogado com sucesso!");

        return BadRequest("Não foi possivel revogar o acesso do usuario");
    }

    [HttpGet("ObterUsuariosPorIdCompanhia/{IdCompanhia}")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult ObterUsuariosPorIdCompanhia(int IdCompanhia)
    {
        var users = _usuarioService.ObterTodosOsUsuarioDaCompanhia(IdCompanhia).Result;
        if (!users.Any())
            return NoContent();

        return Ok(users);
    }

    private bool AtualizarDadosUsuario(UsuarioDTO usuario, AtualizarUsuarioDTO dadosUsuario)
    {
        throw new NotImplementedException();
    }

    [HttpPost("RenovarToken/")]
    [AllowAnonymous]
    public IActionResult RenovarToken(TokenModel token)
    {
        var users = _usuarioService.RenovarTokens(token).Result;
        if (users == null)
            return NoContent();

        return Ok(users);
    }
}