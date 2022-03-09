using Application.Interfaces.Services;
using Application.Responses;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Constants.Storage;
using System.Security.Claims;

namespace Web.Infrastructure.Authentication
{
    public class MegaAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }
        public ICurrentUserService _userService { get; set; }

        public MegaAuthenticationStateProvider(ILocalStorageService localStorageService,
            ICurrentUserService userService)
        {
            _localStorageService = localStorageService;
            _userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>(StorageConstants.LocalStorage.AccessToken);

            ClaimsIdentity identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(accessToken))
            {
                var userModel = await _userService.GetUserFromAccessTokenAsync(accessToken);

                if (userModel != null) // No deberia retornar NULL, evitar referencias nulas. Fijarse si el token es valido primero y despues obtenerlo
                {
                    identity = GetClaimsIdentity(userModel);
                }
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticatedAsync(UserResponse userResponse)
        {
            await _localStorageService.SetItemAsync(StorageConstants.LocalStorage.AccessToken, userResponse.AccessToken);

            var identity = GetClaimsIdentity(userResponse);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await _localStorageService.RemoveItemAsync(StorageConstants.LocalStorage.AccessToken);

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(UserResponse userResponse)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userResponse.AccountName)
            }, "jwt");

            var claimsRoles = userResponse.Permissions.Select(p => new Claim(ClaimTypes.Role, p.Name));
            claimsIdentity.AddClaims(claimsRoles);

            return claimsIdentity;
        }

    }
}
