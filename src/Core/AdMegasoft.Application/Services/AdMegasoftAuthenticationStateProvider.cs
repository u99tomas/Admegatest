using AdMegasoft.Abstractions.Abstractions;
using AdMegasoft.Application.Services.Requests;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AdMegasoft.Application.Services
{
    public class AdMegasoftAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IRoleRepository _roleRepository;

        public AdMegasoftAuthenticationStateProvider(ITokenService tokenService,
            IUserService userService, IRoleRepository roleRepository)
        {
            _tokenService = tokenService;
            _userService = userService;
            _roleRepository = roleRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _tokenService.GetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                return await Task.FromResult(GetAnonymousAuthenticationState());
            }

            var userFromTokenResponse = await _userService.GetUserFromTokenAsync(new UserFromTokenRequest { Token = token });

            if (userFromTokenResponse.FoundAUser)
            {
                return await GetAuthenticationStateAsync(userFromTokenResponse.UserName, userFromTokenResponse.UserId);
            }

            return await Task.FromResult(GetAnonymousAuthenticationState());
        }

        public async Task MarkUserAsAuthenticatedAsync(MarkUserAsAuthenticatedRequest markUserAsAuthenticatedRequest)
        {
            await _tokenService.SaveTokenAsync(markUserAsAuthenticatedRequest.Token);

            var authenticationState = await GetAuthenticationStateAsync(markUserAsAuthenticatedRequest.UserName,
                markUserAsAuthenticatedRequest.UserId);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await _tokenService.DestroyTokenAsync();

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
            var userRoles = await _roleRepository.GetRolesByUserIdAsync(userId);
            return userRoles.Select(r => new Claim(ClaimTypes.Role, r.Name));
        }
    }
}
