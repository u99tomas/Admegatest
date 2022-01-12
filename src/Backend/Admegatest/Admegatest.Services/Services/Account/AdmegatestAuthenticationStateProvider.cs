using Admegatest.Core.Models.Account;
using Admegatest.Services.Interfaces.Account;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Linq;

namespace Admegatest.Services.Services.Account
{
    public class AdmegatestAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorageService { get; }
        private IUserService _userService { get; set; }
        private IRoleService _roleService { get; set; }

        public AdmegatestAuthenticationStateProvider(ILocalStorageService localStorageService,
            IUserService userService, IRoleService roleService)
        {
            _localStorageService = localStorageService;
            _userService = userService;
            _roleService = roleService;
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

            return await Task.FromResult(await GetAuthenticationStateAsync(user));
        }

        public async Task MarkUserAsAuthenticated(User user)
        {
            await _localStorageService.SetItemAsync("token", user.Token);

            var authenticationState = GetAuthenticationStateAsync(user);

            NotifyAuthenticationStateChanged(await Task.FromResult(authenticationState));
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

        private async Task<AuthenticationState> GetAuthenticationStateAsync(User user)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
            }, "apiauth_type");

            var userRoles = await GetAllRolesOfUserAsClaim(user.Id);
            claimsIdentity.AddClaims(userRoles);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationState = new AuthenticationState(claimsPrincipal);

            return authenticationState;
        }

        private async Task<IEnumerable<Claim>> GetAllRolesOfUserAsClaim(int userId)
        {
            var userRoles = await _roleService.GetAllRolesOfUser(userId);
            return userRoles.Select(r => new Claim(ClaimTypes.Role, r.RoleName));
        }
    }
}
