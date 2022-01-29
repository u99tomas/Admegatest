using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Shared.Constants.Storage;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AdMegasoft.Web.Infrastructure.Authentication
{
    public class AdMegasoftAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ILocalStorageService _localStorageService;

        public AdMegasoftAuthenticationStateProvider(IUserService userService,
            IRoleService roleService, ILocalStorageService localStorageService)
        {
            _userService = userService;
            _roleService = roleService;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _localStorageService.GetItemAsync<string>(StorageConstants.LocalStorage.AccessToken);

            if (string.IsNullOrEmpty(token))
            {
                return await Task.FromResult(GetAnonymousAuthenticationState());
            }

            try
            {
                var userFromTokenResponse = await _userService.GetUserFromTokenAsync(token);
                return await GetAuthenticationStateAsync(userFromTokenResponse.UserName, userFromTokenResponse.UserId);
            }
            catch (Exception)
            {
                return await Task.FromResult(GetAnonymousAuthenticationState());
            }
        }

        public async Task MarkUserAsAuthenticatedAsync(int userId, string userName, string token)
        {
            await _localStorageService.SetItemAsync(StorageConstants.LocalStorage.Token, token);

            var authenticationState = await GetAuthenticationStateAsync(userName, userId);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await _localStorageService.RemoveItemAsync(StorageConstants.LocalStorage.Token);

            var authenticationState = GetAnonymousAuthenticationState();

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        #region Private methods
        private AuthenticationState GetAnonymousAuthenticationState()
        {
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(claimsPrincipal);
            return authenticationState;
        }

        private async Task<AuthenticationState> GetAuthenticationStateAsync(string name, int userId)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, name),
            });

            var userRoles = await GetAllRolesOfUserAsClaimAsync(userId);
            claimsIdentity.AddClaims(userRoles);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationState = new AuthenticationState(claimsPrincipal);

            return authenticationState;
        }

        private async Task<IEnumerable<Claim>> GetAllRolesOfUserAsClaimAsync(int userId)
        {
            var userRoles = await _roleService.GetRolesByUserIdAsync(userId);
            return userRoles.Select(r => new Claim(ClaimTypes.Role, r.Name));
        }
        #endregion
    }
}
