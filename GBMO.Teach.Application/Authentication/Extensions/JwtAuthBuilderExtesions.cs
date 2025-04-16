using GBMO.Teach.Application.Authentication.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GBMO.Teach.Application.Authentication.Extensions
{
    public static class JwtAuthBuilderExtesions
    {

        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration(configuration);

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme
            )
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey)),

                    RequireExpirationTime = true,
                };
                //x.Events = new JwtBearerEvents
                //{
                //    OnMessageReceived = context =>
                //    {
                //        string authorization = context.Request.Headers["Authorization"];

                //        if (string.IsNullOrEmpty(authorization))
                //        {
                //            context.NoResult();
                //        }
                //        else
                //        {
                //            context.Token = authorization.Replace("Bearer ", string.Empty);
                //        }

                //        return Task.CompletedTask;
                //    },
                //};
            });
        }
    }
}
