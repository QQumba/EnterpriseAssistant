using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Application.Features.DepartmentFeatures;

[ApiController]
[Route("api/department")]
public class AddUserController : ControllerBase
{
    public AddUserController()
    {

    }

    public async Task<ActionResult> AddUser([FromRoute] long departmentId)
    {
        throw new NotImplementedException();
    }
}
