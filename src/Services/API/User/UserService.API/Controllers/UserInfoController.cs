using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/user-info")]
public class UserInfoController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        var userClaims = User.Claims.Select(c => $"{c.Type}: {c.Value}");
        return Ok(userClaims);
    }
}