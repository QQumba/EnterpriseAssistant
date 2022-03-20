using System.Threading.Tasks;
using EnterpriseAssistant.Identity.DataAccess;
using EnterpriseAssistant.Identity.DataAccess.Security;
using EnterpriseAssistant.Identity.DTOs;

namespace EnterpriseAssistant.Identity.Services
{
	public class AuthService
	{
		private readonly IUserRepository _repository;

		public AuthService(IUserRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> IsUserValidAsync(LoginDto loginDto)
		{
			var user = await _repository.GetUserByLogin(loginDto.Login);
			if (user is null)
			{
				return false;
			}

			var secret = UserSecret.Create(user);

			return secret.Verify(loginDto.Password);
		}
	}
}
