using AdMegasoft.Application.Exceptions;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IJWTService
    {
        string GenerateAccessToken(int userId);

        /// <summary>
        /// Obtiene el id del usuario almacenado dentro del token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="InvalidTokenException"></exception>
        int GetUserIdFromToken(string token);
    }
}
