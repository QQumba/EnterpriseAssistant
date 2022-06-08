using System.Threading.Tasks;
using EnterpriseAssistant.Identity.DataTransfer;
using EnterpriseAssistant.Identity.DTOs;
using EnterpriseAssistant.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Identity.Quickstart.Account
{
	[Route("admin")]
	[ApiController]
	public class AdministrationController : ControllerBase
	{
		private readonly UserService _userService;

		public AdministrationController(UserService userService)
		{
			_userService = userService;
		}

		[HttpPost("create")]
		public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto userDto)
		{
			var user = await _userService.GetUserByEmailAsync(userDto.Email);
			if (user is not null)
			{
				return BadRequest($"User with login: {userDto.Email} already exist");
			}

			var createdUser = await _userService.CreateUserAsync(userDto);
			if (createdUser is null)
			{
				return BadRequest("User cannot be created");
			}

			return Created(nameof(CreateUser), createdUser);
		}
	}
}
