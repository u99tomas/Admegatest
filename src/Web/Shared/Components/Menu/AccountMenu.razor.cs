using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Infrastructure.Authentication;

namespace Web.Shared.Components.Menu
{
    public partial class AccountMenu
    {
        [Inject]
        private AuthenticationStateProvider _authProvider { get; set; }

        protected async Task SignOff()
        {
            var megaAuthProvider = (MegaAuthenticationStateProvider)_authProvider;
            await megaAuthProvider.MarkUserAsLoggedOutAsync();
        }
    }
}
