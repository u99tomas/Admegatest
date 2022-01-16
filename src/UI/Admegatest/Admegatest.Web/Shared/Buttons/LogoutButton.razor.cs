using Admegatest.Services.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Admegatest.Web.Shared.Buttons
{
    public partial class LogoutButton
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; }
        public async Task LogoutAsync()
        {
            var admegatestAuthenticationStateProvider = (AdmegatestAuthenticationStateProvider)_authenticationStateProvider;
            await admegatestAuthenticationStateProvider.MarkUserAsLoggedOutAsync();
            _navigationManager.NavigateTo("/login");
        }
    }
}
