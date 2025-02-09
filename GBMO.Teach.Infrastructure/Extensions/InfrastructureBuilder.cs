using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.UnitOfWorks;
using GBMO.Teach.Infrastructure.Context;
using GBMO.Teach.Infrastructure.Repositories;
using GBMO.Teach.Infrastructure.UnitOfWork;
using GBMO.Teach.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

            return services;
        }
    }
}
