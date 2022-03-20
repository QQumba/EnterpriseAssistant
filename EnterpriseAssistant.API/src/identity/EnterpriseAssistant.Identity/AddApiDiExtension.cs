using EnterpriseAssistant.Identity.Requirements;
using EnterpriseAssistant.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseAssistant.Identity
{
	public static class AddApiDiExtension
	{
		public static void AddApi(this IServiceCollection services)
		{
			services.AddScoped<UserService>();
			services.AddScoped<AuthService>();
			services.AddSingleton<IAuthorizationHandler, AdministrationHandler>();
			services.AddAutoMapper(typeof(AddApiDiExtension));
		}
	}
}
