using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace GBMO.Teach.Application.Authentication.Configurations
{
    public class JwtConfiguration
    {
        public string Issuer { get; } = string.Empty;

        public string SecretKey{ get; } = string.Empty;

        public string Audience { get; } = string.Empty;

        public int ExpireDays { get; }

        public JwtConfiguration(IConfiguration configuration)
        {
            var section = configuration.GetSection("JWT");

            Issuer = section[nameof(Issuer)];
            SecretKey = section[nameof(SecretKey)];
            Audience = section[nameof(Audience)];
            ExpireDays = Convert.ToInt32(section[nameof(ExpireDays)], CultureInfo.InvariantCulture);
        }
    }
}
