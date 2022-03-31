using AssetManager.Interfaces;
using AssetManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetController : ControllerBase
{
    private IAssetService _assetService;
    public AssetController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpPost("Create")]
    public IActionResult Create(AssetModel asset)
    {
        var restultado = _assetService.Create(asset);
        if (restultado != null)
        {
            return Ok(restultado);
        }

        return BadRequest("Erro, não foi possivel criar o Asset");
    }
    
    [HttpPatch("Update")]
    public IActionResult Update(AssetModel asset)
    {    var restultado = _assetService.Update(asset);
        if (restultado != null)
        {
            return Ok(restultado);
        }

        return BadRequest("Erro, não foi possivel criar o Asset");
        
    }
}