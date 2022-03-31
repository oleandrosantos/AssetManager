using AssetManager.Interfaces;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;
[ApiController]
[Route("[controller]")]
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
    public IActionResult CadatrarUsuario(CreateUserViewModel dadosUsuario)
    {
        var resultado = _userService.Create(dadosUsuario);
        if (resultado == null)
        {
            return BadRequest("este email encontra-se cadastrado em nosos banco!");
        }
        
        return Ok(resultado);
   
    }

    [HttpPost("Login")]
    public Task<ActionResult<dynamic>> Authenticate([FromBody]AuthenticationModel model)
    {
        if (string.IsNullOrEmpty(model.email) || string.IsNullOrEmpty(model.password))
        {
            return Task.FromResult<ActionResult<dynamic>>(NotFound(new {message = "Email ou senha não informados"}));
        }
        var resultadoLogin = _userService.Login(model.email, model.password);
        
        if (resultadoLogin.logado == true)
        {
            var usuarioModel = _userService.BuscarPorEmail(model.email);
            var token = _tokenService.GenerateToken(usuarioModel);

            usuarioModel.password = "";
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
    
}