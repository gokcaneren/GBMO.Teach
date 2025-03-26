using GBMO.Teach.Application.Authentication.Configurations;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Services.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GBMO.Teach.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtConfiguration _jwtConfiguration;

        public AuthService(JwtConfiguration jwtConfiguration, IHttpContextAccessor httpContextAccessor)
        {
            _jwtConfiguration = jwtConfiguration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken(User user)
        {
            var claims = CreateClaims(user);
            var creds = CreateCredentials(_jwtConfiguration.SecretKey);

            var token = BuildSecurityToken(_jwtConfiguration, claims, creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> CreateClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleTypeId.ToString())
            };

            return claims;
        }


        private SigningCredentials CreateCredentials(string secretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return creds;
        }

        private JwtSecurityToken BuildSecurityToken(JwtConfiguration jwtConfiguration, List<Claim> claims, SigningCredentials creds)
        {
            return new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwtConfiguration.ExpireDays),
                signingCredentials: creds
            );
        }

        public string? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
