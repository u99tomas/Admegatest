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
            string token = await _localStorageService.GetItemAsync<string>("token");

            if (string.IsNullOrEmpty(token))
            {
                return await Task.FromResult(GetAnonymousAuthenticationState());
            }

            var user = await _userService.GetUserByToken(token);

            if (user == null)
            {
                return await Task.FromResult(GetAnonymousAuthenticationState());
            }

            return await Task.FromResult(GetAuthenticationState(user));
        }

        public async Task MarkUserAsAuthenticated(UserWithToken userWithToken)
        {
            await _localStorageService.SetItemAsync("token", userWithToken.Token);

            var authenticationState = GetAuthenticationState(userWithToken);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("token");

            var authenticationState = GetAnonymousAuthenticationState();

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        private AuthenticationState GetAnonymousAuthenticationState()
        {
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(claimsPrincipal);
            return authenticationState;
        }

        private AuthenticationState GetAuthenticationState(User user)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.RoleDescription),
            }, "apiauth_type");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationState = new AuthenticationState(claimsPrincipal);

            return authenticationState;
        }
    }
}
