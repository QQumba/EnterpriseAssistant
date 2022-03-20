using System.Threading.Tasks;
using EnterpriseAssistant.Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;

namespace EnterpriseAssistant.Identity.Requirements
{
    public class AdministrationRequirement : IAuthorizationRequirement
    {
    }

    public class AdministrationHandler : AuthorizationHandler<AdministrationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AdministrationRequirement requirement)
        {
            // todo: validate user for administrator rights
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}