using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseService.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/enterprise")]
public class EnterpriseUserController : ControllerBase
{
    
}