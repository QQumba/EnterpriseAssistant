using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/user-info")]
[Authorize]
public class UserInfoController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        var userClaims = User.Claims.Select(c => $"{c.Type}: {c.Value}");
        return Ok(userClaims);
    }

    
    [AllowAnonymous]
    [HttpGet("tenant")]
    public IActionResult GetTenant()
    {
        var tenant = HttpContext.Items["auth_enterprise_id"];
        return Ok(tenant);
    }
}