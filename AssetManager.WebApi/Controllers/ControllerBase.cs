using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssetManager.Controllers;
public abstract class Controller : ControllerBase
{
    protected string? ObterEmailUsuario()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claim = identity.Claims;

        var emailUser = claim.Where(x => x.Type == ClaimTypes.Email)
                                .FirstOrDefault();

        return emailUser.Value;
    }
}
