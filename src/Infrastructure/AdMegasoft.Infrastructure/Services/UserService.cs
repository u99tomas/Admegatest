using AdMegasoft.Application.Exceptions;
using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Application.Requests;
using AdMegasoft.Application.Responses;
using AdMegasoft.Shared.Constants.Storage;
using Blazored.LocalStorage;

namespace AdMegasoft.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jWTService;
        private readonly ILocalStorageService _localStorageService;

        public UserService(IUserRepository userRepository, IJWTService jWTService, ILocalStorageService localStorageService)
        {
            _userRepository = userRepository;
            _jWTService = jWTService;
            _localStorageService = localStorageService;
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

        public async Task<bool> LoginAsync(LoginAttemptRequest loginAttemptRequest)
        {
            var userFound = await _userRepository
                .GetActiveUserByPasswordNameAsync(loginAttemptRequest.Name, loginAttemptRequest.Password);

            if (userFound == null) return false;

            await _localStorageService.SetItemAsync(StorageConstants.LocalStorage.Token, _jWTService.GenerateAccessToken(userFound.Id));

            return true;
        }

        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync(StorageConstants.LocalStorage.Token);
        }
    }
}
