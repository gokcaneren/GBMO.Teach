using AutoMapper;
using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Teachers;

namespace GBMO.Teach.Application.Mappings.Auth.Teachers
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<TeacherSchedule, TeacherClassOutput>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Student.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Student.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Student.User.Email));
        }
    }
}
