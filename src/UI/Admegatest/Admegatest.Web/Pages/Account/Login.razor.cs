using Admegatest.Core.Models.Account;
using Admegatest.Services.Interfaces.Account;
using Admegatest.Services.Services.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;

namespace Admegatest.Web.Pages.Account
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

        private MudTextField<string> _userField;
        private MudTextField<string> _passwordField;

        private bool _success;
        private string? _error = null;
        private bool _loading = false;

        protected async override Task OnInitializedAsync()
        {
            await CheckIfUserIsAuthenticatedAsync();
        }

        private async Task CheckIfUserIsAuthenticatedAsync()
        {
            var user = await GetUserAsync();

            if (user.Identity.IsAuthenticated)
            {
                ShowLoadingButton();
                RedirectToRoleHomePage(user);
            }
        }

        private async Task LoginAsync()
        {
            _error = null;
            ShowLoadingButton();

            var userFromForm = new User { Name = _userField.Value, Password = _passwordField.Value };
            var returnedUser = await _userService.LoginAsync(userFromForm);

            if (returnedUser == null)
            {
                HideLoadingButton();
                ShowErrorInvalidUser();
            }
            else
            {
                var admegatestAuthenticationStateProvider = (AdmegatestAuthenticationStateProvider)_authenticationStateProvider;
                await admegatestAuthenticationStateProvider.MarkUserAsAuthenticatedAsync(returnedUser);
                var user = await GetUserAsync();
                RedirectToRoleHomePage(user);
            }
        }

        private void ShowLoadingButton()
        {
            _loading = true;
            StateHasChanged();
        }

        private void HideLoadingButton()
        {
            _loading = false;
            StateHasChanged();
        }

        private void ShowErrorInvalidUser()
        {
            var invalidUser = "El usuario ingresado es incorrecto.";
            _error = invalidUser;
            StateHasChanged();
        }

        private void RedirectToRoleHomePage(ClaimsPrincipal user)
        {
            if (user.IsInRole("IsCustomer"))
            {
                _navigationManager.NavigateTo("/customer/home");
                return;
            }

            if (user.IsInRole("IsEmployee"))
            {
                _navigationManager.NavigateTo("/employee/home");
                return;
            }
        }

        private async Task<ClaimsPrincipal> GetUserAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user;
        }
    }
}
