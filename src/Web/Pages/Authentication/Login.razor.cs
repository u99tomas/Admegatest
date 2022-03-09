using Application.Interfaces.Services;
using Application.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Infrastructure.Authentication;

namespace Web.Pages.Authentication
{
    public partial class Login
    {
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        private ICurrentUserService _userService { get; set; }

        private bool _loading = false;

        private TokenRequest _model = new();

        private bool _userEnteredIsIncorrect;

        private async Task LoginAsync()
        {
            ToggleLoading();
            var userModel = await _userService.LoginAsync(_model);

            if (userModel == null)
            {
                ToggleLoading();
                _userEnteredIsIncorrect = true;
            }
            else
            {
                var adMegasoftAuthenticationStateProvider = (MegaAuthenticationStateProvider)_authenticationStateProvider;
                await adMegasoftAuthenticationStateProvider.MarkUserAsAuthenticatedAsync(userModel);
                _navigationManager.NavigateTo("/content/dashboard");
            }
        }

        private void ToggleLoading()
        {
            _loading = !_loading;
            StateHasChanged();
        }
    }
}
