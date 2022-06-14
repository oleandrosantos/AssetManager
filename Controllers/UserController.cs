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
        dadosUsuario.Email = dadosUsuario.Email.ToLower().Trim();
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
        model.Email = model.Email.ToLower().Trim();
        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            return Task.FromResult<ActionResult<dynamic>>(NotFound(new { message = "Email ou senha não informados" }));
        }
        var resultadoLogin = _userService.Login(model.Email, model.Password);
        
        if (resultadoLogin.Status)
        {
            var usuarioModel = _userService.BuscarPorEmail(model.Email);
            usuarioModel.Password = "";
            var token = _tokenService.GenerateToken(usuarioModel);
            return Task.FromResult<ActionResult<dynamic>>(new
            {
                token = token,
            });
        }
        else
        {
            return Task.FromResult<ActionResult<dynamic>>(BadRequest(resultadoLogin.Mensagem));
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