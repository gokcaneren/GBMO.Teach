using AutoMapper;
using GBMO.Teach.Application.Utilities;
using GBMO.Teach.Core.DTOs.Input.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Student;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Application.Mappings.Auth.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterInput, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordManager.Hash(src.Password)))
                .AfterMap<UserRoleMappingAction>();

            CreateMap<User, UserLoginOutput>();

            CreateMap<User, NonSubTeacherOutput>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src=> src.Teacher.Id))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Teacher.Bio))
                .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.Teacher.HourlyRate));

            CreateMap<User, StudentUserSimpleOutput>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src=> src.Student.Id));
        }
    }

    public class UserRoleMappingAction : IMappingAction<UserRegisterInput, User>
    {
        public void Process(UserRegisterInput source, User destination, ResolutionContext context)
        {
            object role = source.RoleTypeId switch
            {
                PublicRole.Teacher => destination.Teacher = new Teacher()
                { Bio = source.UserTeacherDetail?.Bio, HourlyRate = source.UserTeacherDetail?.HourlyRate },
                _ => destination.Student = new Student()
            };
        }
    }

}
