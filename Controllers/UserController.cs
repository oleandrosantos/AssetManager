using AssetManager.Data;
using AssetManager.Model;
using AssetManager.Service;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private UserService _service;

    [HttpPost("Create")]
    public IActionResult CadatrarUsuario(UserModel usuario)
    {
        if (!TryValidateModel(usuario))
        {
            return BadRequest();
        }
        var resultado = _service.Create(usuario);
        return Accepted(resultado);
    }
    
}