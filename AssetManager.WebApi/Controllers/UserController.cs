using AssetManager.Application.DTO.User;
using AssetManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace AssetManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

  private IUserService _userService;
  private ITokenService _tokenService;

  public UserController(IUserService userService, ITokenService tokenService)
  {
    _userService = userService;
    _tokenService = tokenService;
  }

  [HttpPost("Create")]
  [AllowAnonymous]
  public IActionResult CadastrarUsuario(CreateUserDTO dadosUsuario)
  {
    dadosUsuario.Email = dadosUsuario.Email.ToLower().Trim();
    var resultado = _userService.Create(dadosUsuario).Result;
    if (string.IsNullOrEmpty(resultado))
      return BadRequest("Este email encontra-se cadastrado em nosos banco!");

    return Ok(resultado);
  }

  [HttpPost("Login")]
  [AllowAnonymous]
  public IActionResult Authenticate([FromBody] AuthenticationDTO model)
  {
        try
        {
            model.Email = model.Email.ToLower().Trim();
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
              return NotFound(new { message = "Email ou senha não informados" });
            }
            var resultadoLogin = _userService.Login(model.Email, model.Password);

            var usuarioModel = _userService.BuscarPorEmail(model.Email).Result;
            usuarioModel!.Password = "";
            var tokenJWT = _tokenService.GenerateToken(usuarioModel);
            return Ok(new { token = tokenJWT });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possivel realizar o login" });
        }
  }

  [HttpPut("Update")]
  [Authorize(Roles = "Administrador,Suporte,Funcionario")]
  public IActionResult UpdateUser(UpdateUserDTO dadosUsuario)
  {
        throw new NotImplementedException();
    //UserModel? usuario = _userService.BuscarPorEmail(dadosUsuario.Email);

    //if (usuario == null || !AtualizarDadosUsuario(usuario, dadosUsuario))
    //  return BadRequest("Não foi possivel atualizar o usuario");

    //return Ok("Usuario atualizado com sucesso");
  }

  [HttpPut("RevogarAcesso")]
  [Authorize(Roles = "Administrador,Suporte")]
  public IActionResult RevogarAcesso(string email)
  {
        throw new NotImplementedException();
    //    UserModel? usuario = _userService.BuscarPorEmail(email);
    //if (usuario == null)
    //  return BadRequest("Não conseguimos localizar este usuario!");

    //usuario.isActive = false;

    //if (!_userService.UpdateUser(usuario))
    //  return BadRequest("Não foi possivel revogar o acesso");

    //return Ok("Acesso revogado com sucesso");
  }

  private bool AtualizarDadosUsuario(UserDTO usuario, UpdateUserDTO dadosUsuario)
  {
        throw new NotImplementedException();
    //    usuario.Name = dadosUsuario.name ?? usuario.Name;
    //usuario.Password = dadosUsuario.password ?? usuario.Password;
    //usuario.Role = dadosUsuario.role ?? usuario.Role;
    //return _userService.UpdateUser(usuario);
  }
}