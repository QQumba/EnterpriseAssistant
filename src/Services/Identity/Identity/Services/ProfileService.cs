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
			var email = context.Subject.GetSubjectId();
			var user = await _userService.GetUserByEmailAsync(email);

			context.IssuedClaims.Add(new Claim("user_id", user.Id.ToString()));
			context.IssuedClaims.Add(new Claim("email", user.Email));
			context.IssuedClaims.Add(new Claim("first_name", user.FirstName));
			context.IssuedClaims.Add(new Claim("last_name", user.LastName));

			if (user.EnterpriseIds is not null)
			{
				context.IssuedClaims.Add(new Claim("enterprise_ids", user.EnterpriseIds));
			}
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			var login = context.Subject.GetSubjectId();
			var user = await _userService.GetUserByEmailAsync(login);
			if (user is null)
			{
				return;
			}

			context.IsActive = !user.IsSoftDeleted;
		}
	}
}
