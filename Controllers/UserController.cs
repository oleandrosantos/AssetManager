using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private ITokenService _tokenService;
    private IMapper _mapper;

    public UserController(IUserService userService, ITokenService tokenService, IMapper mapper)
    {
        _userService = userService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    [AllowAnonymous]
    public IActionResult CadatrarUsuario(CreateUserViewModel dadosUsuario)
    {
        dadosUsuario.email = dadosUsuario.email.ToLower().Trim();
        var resultado = _userService.Create(dadosUsuario);
        if (resultado == null)
        {
            return BadRequest("este email encontra-se cadastrado em nosos banco!");
        }

        return Ok(resultado);

    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public Task<ActionResult<dynamic>> Authenticate([FromBody] AuthenticationModel model)
    {
        model.email = model.email.ToLower().Trim();
        if (string.IsNullOrEmpty(model.email) || string.IsNullOrEmpty(model.password))
        {
            return Task.FromResult<ActionResult<dynamic>>(NotFound(new { message = "Email ou senha não informados" }));
        }
        var resultadoLogin = _userService.Login(model.email, model.password);

        if (resultadoLogin.status)
        {
            var usuarioModel = _userService.BuscarPorEmail(model.email);
            usuarioModel.password = "";
            var token = _tokenService.GenerateToken(usuarioModel);
            return Task.FromResult<ActionResult<dynamic>>(new
            {
                token = token,
            });
        }
        else
        {
            return Task.FromResult<ActionResult<dynamic>>(BadRequest(resultadoLogin.mensagem));
        }

    }

    [HttpPut("Update")]
    [Authorize(Roles = "Administrador,Suporte,Funcionario")]
    public IActionResult UpdateUser(UpdateUserViewModel dadosUsuario)
    {
        UserModel usuario = _userService.BuscarPorEmail(dadosUsuario.email);
        usuario.name = dadosUsuario.name ?? usuario.name;
        usuario.password = dadosUsuario.password ?? usuario.password;
        usuario.role = dadosUsuario.role ?? usuario.role;
        if (!_userService.UpdateUser(usuario))
        {
            return BadRequest("Não foi possivel atualizar o usuario");
        }

        return Ok("Usuario atualizado com sucesso");
    }

    [HttpPut("RevogarAcesso")]
    [Authorize(Roles = "Administrador,Suporte")]
    public IActionResult RevogarAcesso(string email)
    {
        UserModel usuario = _userService.BuscarPorEmail(email);
        usuario.isActive = false;
        if (!_userService.UpdateUser(usuario))
        {
            return BadRequest("Não foi possivel revogar o acesso");
        }

        return Ok("Acesso revogado com sucesso");
    }
}