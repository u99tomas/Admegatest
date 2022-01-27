namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IJWTService
    {
        string GenerateAccessToken(int userId);
        int? GetUserIdFromToken(string token);
    }
}
