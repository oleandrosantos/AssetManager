using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
  public IActionResult CadastrarUsuario(CreateUserViewModel dadosUsuario)
  {
    dadosUsuario.Email = dadosUsuario.Email.ToLower().Trim();
    var resultado = _userService.Create(dadosUsuario);
    if (string.IsNullOrEmpty(resultado))
      return BadRequest("Este email encontra-se cadastrado em nosos banco!");

    return Ok(resultado);
  }

  [HttpPost("Login")]
  [AllowAnonymous]
  public Task<ActionResult> Authenticate([FromBody] AuthenticationModel model)
  {
    model.Email = model.Email.ToLower().Trim();
    if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
    {
      return Task.FromResult<ActionResult>(NotFound(new { message = "Email ou senha não informados" }));
    }
    var resultadoLogin = _userService.Login(model.Email, model.Password);

    if (resultadoLogin.Status)
    {
      var usuarioModel = _userService.BuscarPorEmail(model.Email);
      usuarioModel!.Password = "";
      var tokenJWT = _tokenService.GenerateToken(usuarioModel);
      return Task.FromResult<ActionResult>(Ok(new { token = tokenJWT }));
    }
    else
    {
      return Task.FromResult<ActionResult>(BadRequest(new { message = resultadoLogin.Mensagem }));
    }

  }

  [HttpPut("Update")]
  [Authorize(Roles = "Administrador,Suporte,Funcionario")]
  public IActionResult UpdateUser(UpdateUserViewModel dadosUsuario)
  {
    UserModel? usuario = _userService.BuscarPorEmail(dadosUsuario.email);

    if (usuario == null || !AtualizarDadosUsuario(usuario, dadosUsuario))
      return BadRequest("Não foi possivel atualizar o usuario");

    return Ok("Usuario atualizado com sucesso");
  }

  [HttpPut("RevogarAcesso")]
  [Authorize(Roles = "Administrador,Suporte")]
  public IActionResult RevogarAcesso(string email)
  {
    UserModel? usuario = _userService.BuscarPorEmail(email);
    if (usuario == null)
      return BadRequest("Não conseguimos localizar este usuario!");

    usuario.isActive = false;

    if (!_userService.UpdateUser(usuario))
      return BadRequest("Não foi possivel revogar o acesso");

    return Ok("Acesso revogado com sucesso");
  }

  private bool AtualizarDadosUsuario(UserModel usuario, UpdateUserViewModel dadosUsuario)
  {
    usuario.Name = dadosUsuario.name ?? usuario.Name;
    usuario.Password = dadosUsuario.password ?? usuario.Password;
    usuario.Role = dadosUsuario.role ?? usuario.Role;
    return _userService.UpdateUser(usuario);
  }
}