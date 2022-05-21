using AutoMapper;
using EnterpriseAssistant.Identity.DataAccess.Entities;
using EnterpriseAssistant.Identity.DTOs;
using EnterpriseAssistant.Identity.Quickstart.Account;

namespace EnterpriseAssistant.Identity.MappingProfiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<UserDto, User>().ReverseMap();
			CreateMap<LoginDto, User>().ReverseMap();
			CreateMap<SignupViewModel, UserDto>().ReverseMap();
			CreateMap<IdentityUserCreateDto, User>();
		}
	}
}
