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
        return Accepted(resultado);
    }
    
}