using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.Services.CommonServices;
using GBMO.Teach.Core.Services.ConfigurationsServices;
using GBMO.Teach.Core.Services.StudentServices;
using GBMO.Teach.Core.Services.TeacherServices;
using GBMO.Teach.Core.Services;
using GBMO.Teach.Core.UnitOfWorks;
using GBMO.Teach.Infrastructure.Context;
using GBMO.Teach.Infrastructure.Repositories;
using GBMO.Teach.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Infrastructure.Repositories.AuthRepositories;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Infrastructure.Repositories.CommonRepositories;
using GBMO.Teach.Core.Repositories.ConfigurationsRepositories;
using GBMO.Teach.Infrastructure.Repositories.ConfigurationsRepositories;
using GBMO.Teach.Core.Repositories.StudentRepositories;
using GBMO.Teach.Infrastructure.Repositories.StudentRepositories;
using GBMO.Teach.Core.Repositories.TeacherRepositories;
using GBMO.Teach.Infrastructure.Repositories.TeacherRepositories;

namespace GBMO.Teach.Infrastructure.Extensions
{
    public static class InfrastructureBuilder
    {
        public static IServiceCollection BuildInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GbmoDbContext>(options =>
                options.UseNpgsql(configuration["ConnectionStrings:GBMOTech"], c =>
                c.MigrationsAssembly(Assembly.GetAssembly(typeof(GbmoDbContext))!.GetName().Name)));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IClassBookingRepository, ClassBookingRepository>();
            services.AddScoped<ITeacherStudentConnectionRepository, TeacherStudentConnectionRepository>();

            services.AddScoped<ISettingRepository, SettingRepository>();

            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITeacherScheduleRepository, TeacherScheduleRepository>();

            return services;
        }
    }
}
