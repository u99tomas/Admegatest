using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Application.Models;
using AdMegasoft.Shared.Constants.Storage;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AdMegasoft.Web.Infrastructure.Authentication
{
    public class AdMegasoftAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }
        public IUserService _userService { get; set; }

        public AdMegasoftAuthenticationStateProvider(ILocalStorageService localStorageService,
            IUserService userService)
        {
            _localStorageService = localStorageService;
            _userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>(StorageConstants.Local.AccessToken);

            ClaimsIdentity identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(accessToken))
            {
                var userModel = await _userService.GetUserFromAccessTokenAsync(accessToken);

                if (userModel != null) // TODO: No deberia retornar NULL, evitar referencias nulas. Fijarse si el token es valido primero y despues obtenerlo
                {
                    identity = GetClaimsIdentityAsync(userModel);
                }
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(UserModel userModel)
        {
            await _localStorageService.SetItemAsync(StorageConstants.Local.AccessToken, userModel.AccessToken);

            var identity = GetClaimsIdentityAsync(userModel);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync(StorageConstants.Local.AccessToken);

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentityAsync(UserModel userModel)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userModel.Name),
            });

            var claimsRoles = userModel.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name));
            claimsIdentity.AddClaims(claimsRoles);

            return claimsIdentity;
        }

    }
}