using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using EnterpriseAssistant.Identity.DataAccess;
using EnterpriseAssistant.Identity.DataAccess.Entities;
using EnterpriseAssistant.Identity.DataAccess.Security;
using EnterpriseAssistant.Identity.DTOs;
using Microsoft.Extensions.Configuration;

namespace EnterpriseAssistant.Identity.Services
{
	public class UserService
	{
		private readonly IUserRepository _repository;
		private readonly IMapper _mapper;
		private readonly IHttpClientFactory _factory;
		private readonly IConfiguration _configuration;

		public UserService(IUserRepository repository, IMapper mapper, IHttpClientFactory factory, IConfiguration configuration)
		{
			_repository = repository;
			_mapper = mapper;
			_factory = factory;
			_configuration = configuration;
		}

		public async Task<UserDto> GetUserByLoginAsync(string login)
		{
			var user = await _repository.GetUserByLogin(login);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> CreateUserAsync(IdentityUserCreateDto userDto)
		{
			var user = _mapper.Map<User>(userDto);
			var secret = UserSecret.Create(user.Password);

			user.Salt = secret.Salt;
			user.Password = secret.PasswordHash;

			var createdUser = await _repository.CreateUserAsync(user);
			return _mapper.Map<UserDto>(createdUser);
		}

		public async Task<UserDto> UpdateUserAsync(UserDto userDto)
		{
			var user = await _repository.UpdateUser(_mapper.Map<User>(userDto));
			return _mapper.Map<UserDto>(user);
		}
	}
}
