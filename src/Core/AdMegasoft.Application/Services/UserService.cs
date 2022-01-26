using AdMegasoft.Abstractions.Abstractions;
using AdMegasoft.Application.Requests;
using AdMegasoft.Application.Responses;

namespace AdMegasoft.Application.Services
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

        public async Task<LoginAttemptResponse> Login(LoginAttemptRequest loginAttemptRequest)
        {
            var userFound = await _userRepository
                .GetActiveUserByPasswordNameAsync(loginAttemptRequest.Name, loginAttemptRequest.Password);

            if (userFound == null)
            {
                return new LoginAttemptResponse { Success = false };
            }

            return new LoginAttemptResponse
            {
                Success = true,
                Token = _jWTService.GenerateAccessToken(userFound.Id),
                UserId = userFound.Id,
                UserName = userFound.Name,
            };
        }

        public async Task<UserFromTokenResponse> GetUserFromTokenAsync(string token)
        {
            var userId = _jWTService.GetUserIdFromToken(token);

            if (userId == null)
            {
                return new UserFromTokenResponse { FoundAUser = false };
            }

            var foundUser = await _userRepository.GetByIdAsync(userId.Value);

            if (foundUser == null)
            {
                return new UserFromTokenResponse { FoundAUser = false };
            }

            return new UserFromTokenResponse
            {
                FoundAUser = true,
                UserId = userId.Value,
                UserName = foundUser.Name,
            };
        }
    }
}
