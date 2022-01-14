using Admegatest.Services.Services.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Web.Components.Buttons
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
