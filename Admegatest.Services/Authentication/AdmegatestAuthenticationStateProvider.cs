using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Admegatest.Core.Models;
using Admegatest.Core.Models.AuthenticationAndAuthorization;
using Admegatest.Services.IServices;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Admegatest.Services.Authentication
{
    public class AdmegatestAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorageService { get; }
        private IUserService _userService { get; set; }

        public AdmegatestAuthenticationStateProvider(ILocalStorageService localStorageService,
            IUserService userService)
        {
            _localStorageService = localStorageService;
            _userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string accessToken = await _localStorageService.GetItemAsync<string>("accessToken");

            ClaimsIdentity identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(accessToken))
            {
                User user = await _userService.GetUserByAccessToken(accessToken);
                identity = GetClaimsIdentity(user);
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(claimsPrincipal);

            return await Task.FromResult(authenticationState);
        }

        public async Task MarkUserAsAuthenticated(UserWithToken userWithToken)
        {
            await _localStorageService.SetItemAsync("accessToken", userWithToken.AccessToken);
            await _localStorageService.SetItemAsync("refreshToken", userWithToken.RefreshToken);

            var identity = GetClaimsIdentity(userWithToken);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(claimsPrincipal);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("accessToken");
            await _localStorageService.RemoveItemAsync("refreshToken");

            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(claimsPrincipal);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        private ClaimsIdentity GetClaimsIdentity(User? user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if(user.Name != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.RoleDescription),
                }, "apiauth_type");
            }

            return claimsIdentity;
        }
    }
}
