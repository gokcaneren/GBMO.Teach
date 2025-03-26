using FluentValidation;
using GBMO.Teach.Application.Authentication.Configurations;
using GBMO.Teach.Application.Mappings.Auth.Users;
using GBMO.Teach.Application.Services;
using GBMO.Teach.Application.Services.AuthServices;
using GBMO.Teach.Application.Validations.Auth.User;
using GBMO.Teach.Core.Services;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.Services.CommonServices;
using GBMO.Teach.Core.Services.ConfigurationsServices;
using GBMO.Teach.Core.Services.StudentServices;
using GBMO.Teach.Core.Services.TeacherServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GBMO.Teach.Application.Extensions
{
    public static class ApplicationBuilder
    {
        public static IServiceCollection BuildApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<JwtConfiguration>();

            services.AddHttpContextAccessor();

            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IClassBookingService, ClassBookingService>();
            services.AddScoped<ITeacherStudentConnectionService, TeacherStudentConnectionService>();

            services.AddScoped<ISettingService, SettingService>();

            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITeacherScheduleService, TeacherScheduleService>();

            services.AddAutoMapper(typeof(UserProfile));

            services.AddValidatorsFromAssemblyContaining(typeof(UserRegisterInputValidator));


            return services;
        }
    }
}
