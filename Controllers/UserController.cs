using AssetManager.Data;
using AssetManager.Model;
using AssetManager.Service;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Create")]
    public IActionResult CadatrarUsuario(UserViewModel dadosUsuario)
    {
       
        var resultado = _userService.Create(dadosUsuario);
        if (resultado == null)
        {
            return BadRequest("este email encontra-se cadastrado em nosos banco!");
        }
        
        return Ok(resultado);
   
    }

    [HttpPost("Login")]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody]UserViewModel model)
    {
        if (string.IsNullOrEmpty(model.email) || string.IsNullOrEmpty(model.password))
        {
            return NotFound(new {message = "Email ou senha não informados"});
        }
        var resultadoLogin = _userService.Login(model.email, model.password);
        
        if (resultadoLogin.logado == true)
        {
            var usuarioModel = _userService.BuscarPorEmail(model.email);
            var token = TokenService.GenerateToken(usuarioModel);
            usuarioModel.password = "";
            return new
            {
                usuarioModel = usuarioModel,
                token = token,
            };
        }
        else
        {
            return BadRequest(resultadoLogin.mensagem);
        }
        
    }
    
}