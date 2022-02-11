﻿using System.Threading.Tasks;
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
		public async Task<ActionResult<UserDto>> CreateUser(IdentityUserCreateDto userDto)
		{
			var user = await _userService.GetUserByLoginAsync(userDto.Login);
			if (user is not null)
			{
				return BadRequest($"User with login: {userDto.Login} already exist");
			}

			var createdUser = await _userService.CreateUserAsync(userDto);
			if (createdUser is null)
			{
				return BadRequest("User cannot be created");
			}

			return Created(nameof(CreateUser), createdUser);
		}

		[HttpPut("update")]
		public async Task<ActionResult<UserDto>> UpdateUser(UserDto userDto)
		{
			var user = await _userService.GetUserByLoginAsync(userDto.Login);
			if (user is not null)
			{
				return BadRequest($"User with login: {userDto.Login} not found");
			}

			var createdUser = await _userService.UpdateUserAsync(userDto);
			if (createdUser is null)
			{
				return BadRequest("User cannot be created");
			}

			return Created(nameof(CreateUser), createdUser);
		}
	}
}
