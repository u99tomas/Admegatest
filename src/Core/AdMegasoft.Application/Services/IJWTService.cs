using Microsoft.IdentityModel.Tokens;

namespace AdMegasoft.Application.Services
{
    public interface IJWTService
    {
        string GenerateAccessToken(int userId);
        int? GetUserIdFromToken(string token);
    }
}
