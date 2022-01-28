using AdMegasoft.Application.Requests;
using AdMegasoft.Application.Responses;
using AdMegasoft.Application.Exceptions;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginAttemptRequest loginAttemptRequest);
        Task LogoutAsync();

        /// <summary>
        /// Obtiene el id del usuario almacenado dentro del token.<br></br>
        /// Luego intenta obtener el usuario realizando una búsqueda por id en la base datos.
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="InvalidTokenException"></exception>
        Task<UserFromTokenResponse> GetUserFromTokenAsync(string token);
    }
}
