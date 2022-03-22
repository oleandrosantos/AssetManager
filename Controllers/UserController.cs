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
    public IActionResult LoginUsuario(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return BadRequest("Email ou senha não informados");
        }
        var resultadoLogin = _userService.Login(email, password);
        
        if (resultadoLogin.logado == true)
        {
            return Ok(resultadoLogin.mensagem);
        }
        else
        {
            return BadRequest(resultadoLogin.mensagem);
        }
        
    }
    
}