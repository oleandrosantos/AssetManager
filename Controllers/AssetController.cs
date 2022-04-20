using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.ViewModel;
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
    public IActionResult Create(CreateAsset asset)
    {
        var resultado = _assetService.Create(asset);
        if (resultado != null)
        {
            return Ok(resultado);
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