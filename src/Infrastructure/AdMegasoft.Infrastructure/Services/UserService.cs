using AdMegasoft.Application.Exceptions;
using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Application.Requests;
using AdMegasoft.Application.Responses;

namespace AdMegasoft.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jWTService;

        public UserService(IUserRepository userRepository, IJWTService jWTService)
        {
            _userRepository = userRepository;
            _jWTService = jWTService;
        }

        public async Task<UserFromTokenResponse> GetUserFromTokenAsync(string token)
        {
            var userId = _jWTService.GetUserIdFromToken(token);

            var foundUser = await _userRepository.GetByIdAsync(userId);

            if (foundUser == null)
            {
                throw new UserNotFoundException($"No se ha encontrado en la base de datos un usuario con el id: {userId}");
            }

            return new UserFromTokenResponse
            {
                UserId = userId,
                UserName = foundUser.Name,
            };
        }

        public async Task<LoginAttemptResponse> LoginAsync(LoginAttemptRequest loginAttemptRequest)
        {
            var userFound = await _userRepository
                .GetActiveUserByPasswordNameAsync(loginAttemptRequest.Name, loginAttemptRequest.Password);

            if (userFound == null)
            {
                throw new UserNotFoundException($"No se ha encontrado en la base de datos un usuario con nombre: " +
                    $"{loginAttemptRequest.Name} y contraseña: {loginAttemptRequest.Password}");
            }

            return new LoginAttemptResponse
            {
                UserId = userFound.Id,
                UserName = userFound.Name,
                Token = _jWTService.GenerateAccessToken(userFound.Id),
            };
        }
    }
}
