using AutoMapper;
using EnterpriseAssistant.Identity.DataAccess.Entities;
using EnterpriseAssistant.Identity.DataTransfer;
using EnterpriseAssistant.Identity.Quickstart.Account;

namespace EnterpriseAssistant.Identity.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(u => u.EnterpriseIds,
                    opt => opt.MapFrom(u => string.Join(' ', u.EnterpriseIds)));
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<SignupViewModel, UserDto>().ReverseMap();
            CreateMap<SignupViewModel, UserCreateDto>().ReverseMap();
            CreateMap<UserCreateDto, User>();
        }
    }
}