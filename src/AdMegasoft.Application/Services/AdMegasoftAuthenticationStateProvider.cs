using AdMegasoft.Core.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;

namespace AdMegasoft.Application.Services
{
    public class AdMegasoftAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AdMegasoftAuthenticationStateProvider(ITokenService tokenService,
            IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
