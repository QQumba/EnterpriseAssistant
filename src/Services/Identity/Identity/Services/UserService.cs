using System.Threading.Tasks;
using AutoMapper;
using EnterpriseAssistant.Identity.DataAccess;
using EnterpriseAssistant.Identity.DataAccess.Entities;
using EnterpriseAssistant.Identity.DataAccess.Security;
using EnterpriseAssistant.Identity.DataTransfer;
using EnterpriseAssistant.Identity.DTOs;

namespace EnterpriseAssistant.Identity.Services
{
	public class UserService
	{
		private readonly IUserRepository _repository;
		private readonly IMapper _mapper;

		public UserService(IUserRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<UserDto> GetUserByEmailAsync(string email)
		{
			var user = await _repository.GetUserByEmail(email);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> CreateUserAsync(UserCreateDto userDto)
		{
			var user = _mapper.Map<User>(userDto);
			var secret = UserSecret.Create(user.Password);

			user.Salt = secret.Salt;
			user.Password = secret.PasswordHash;

			var createdUser = await _repository.CreateUserAsync(user);
			return _mapper.Map<UserDto>(createdUser);
		}
	}
}
