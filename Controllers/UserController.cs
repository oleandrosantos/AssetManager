using AssetManager.Interfaces;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private IUserService _userService;
    private ITokenService _tokenService;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService=tokenService;
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
    public Task<ActionResult<dynamic>> Authenticate([FromBody]AuthenticationModel model)
    {
        model.Email = model.Email.ToLower().Trim();
        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            return Task.FromResult<ActionResult<dynamic>>(NotFound(new {message = "Email ou senha não informados"}));
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
    
}