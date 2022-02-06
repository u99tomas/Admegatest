using AdMegasoft.Web.Authentication;
using Application.Interfaces.Services;
using Application.Requests;
using Application.Validators;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace AdMegasoft.Web.Pages.Authentication
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

        #region (Properties) Password field behavior
        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        #endregion

        #region (Properties) Login button behavior
        private bool _loading = false;
        #endregion

        private TokenRequest _model = new();
        private TokenRequestValidator _validator = new();
        private MudForm _form;
        private bool _userEnteredIsIncorrect;

        private async Task LoginAsync()
        {
            await _form.Validate();

            if (!_form.IsValid)
            {
                return;
            }

            ToggleLoading();
            var userModel = await _userService.LoginAsync(_model);

            if (userModel == null)
            {
                ToggleLoading();
                _userEnteredIsIncorrect = true;
            }
            else
            {
                var adMegasoftAuthenticationStateProvider = (AdMegasoftAuthenticationStateProvider)_authenticationStateProvider;
                await adMegasoftAuthenticationStateProvider.MarkUserAsAuthenticatedAsync(userModel);
                _navigationManager.NavigateTo("/user/dashboard");
            }
        }

        #region (Methods) Password field behavior
        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
        #endregion

        #region (Methods) Login button behavior
        private void ToggleLoading()
        {
            _loading = !_loading;
            StateHasChanged();
        }
        #endregion
    }
}
