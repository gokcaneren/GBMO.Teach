using AutoMapper;
using GBMO.Teach.Application.Utilities;
using GBMO.Teach.Core.DTOs.Input.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Auth.User;

namespace GBMO.Teach.Application.Mappings.Auth.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterInput, Core.Entities.Auth.User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordManager.Hash(src.Password)));

            CreateMap<Core.Entities.Auth.User, UserLoginOutput>();
        }
    }
}
