using AdMegasoft.Application.Requests;
using AdMegasoft.Application.Responses;
using AdMegasoft.Application.Exceptions;

namespace AdMegasoft.Application.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginAttemptRequest"></param>
        /// <returns></returns>
        /// <exception cref="UserNotFoundException"></exception>
        Task<LoginAttemptResponse> LoginAsync(LoginAttemptRequest loginAttemptRequest);

        /// <summary>
        /// Obtiene el id del usuario almacenado dentro del token.<br></br>
        /// Busca el usuario realizando una búsqueda por id en la base datos.
        /// </summary>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="InvalidTokenException"></exception>
        Task<UserFromTokenResponse> GetUserFromTokenAsync(string token);
    }
}
