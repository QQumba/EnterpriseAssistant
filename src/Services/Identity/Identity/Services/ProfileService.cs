using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace EnterpriseAssistant.Identity.Services
{
	public class ProfileService : IProfileService
	{
		private readonly UserService _userService;
		private readonly ILogger _logger;

		public ProfileService(ILogger<ProfileService> logger, UserService userService)
		{
			_logger = logger;
			_userService = userService;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var login = context.Subject.GetSubjectId();
			var user = await _userService.GetUserByLoginAsync(login);

			context.IssuedClaims.Add(new Claim("role", user.Role.ToString()));
			context.IssuedClaims.Add(new Claim("login", user.Login));
			context.IssuedClaims.Add(new Claim("name", user.Name));
			context.IssuedClaims.Add(new Claim("enterprise_ids", "test 123"));
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			var login = context.Subject.GetSubjectId();
			var user = await _userService.GetUserByLoginAsync(login);
			if (user is null)
			{
				return;
			}

			context.IsActive = !user.IsSoftDeleted;
		}
	}
}
