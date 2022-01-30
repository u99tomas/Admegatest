using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Web.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AdMegasoft.Web.Pages.Authentication.Login
{
    public partial class Login
    {
        #region Injections
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private IUserService _userService { get; set; }
        #endregion

        private string _user { get; set; } = "";
        private string _password { get; set; } = "";
        private bool _error = false;

        private async Task LoginAsync()
        {
            var userModel = await _userService.LoginAsync(_user, _password);

            if (userModel != null)
            {
                var adMegasoftAuthenticationStateProvider = (AdMegasoftAuthenticationStateProvider)_authenticationStateProvider;
                await adMegasoftAuthenticationStateProvider.MarkUserAsAuthenticated(userModel);
                _navigationManager.NavigateTo("/dashboard");
            }

            _error = true;
        }
    }
}
