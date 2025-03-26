using GBMO.Teach.Core.Entities.Auth;

namespace GBMO.Teach.Core.Services.AuthServices
{
    public interface IAuthService
    {
        string GenerateToken(User user);

        string? GetCurrentUserId();
    }
}
