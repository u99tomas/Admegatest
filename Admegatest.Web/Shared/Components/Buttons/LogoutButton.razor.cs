using Admegatest.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Admegatest.Web.Shared.Components.Buttons
{
    public partial class LogoutButton
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; }
        public async Task Logout()
        {
            var admegatestAuthenticationStateProvider = (AdmegatestAuthenticationStateProvider)_authenticationStateProvider;
            await admegatestAuthenticationStateProvider.MarkUserAsLoggedOut();
            _navigationManager.NavigateTo("/login");
        }
    }
}
