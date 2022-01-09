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
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private IUserService _userService { get; set; }

        private bool _success;
        private MudTextField<string> _userField;
        private MudTextField<string> _passwordField;
        private List<string> Errors = new List<string>();
        private bool _loggingIn = false;

        protected async override Task OnInitializedAsync()
        {
            await CheckIfUserIsAuthenticated();
        }

        private async Task CheckIfUserIsAuthenticated()
        {
            var user = await GetUserAsync();

            if (user.Identity.IsAuthenticated)
            {
                ShowLoadingButton();
                RedirectToRoleHomePage(user);
            }
        }

        private async void ValidateUser()
        {
            Errors.Clear();
            ShowLoadingButton();
            var userFromForm = GetUserFromForm();
            var returnedUser = await _userService.Login(userFromForm);

            if (returnedUser == null)
            {
                HideLoadingButton();
                ShowErrorInvalidUser();
            }
            else
            {
                var admegatestAuthenticationStateProvider = (AdmegatestAuthenticationStateProvider)_authenticationStateProvider;
                await admegatestAuthenticationStateProvider.MarkUserAsAuthenticated(returnedUser);
                var user = await GetUserAsync();
                RedirectToRoleHomePage(user);
            }

        }

        private void ShowLoadingButton()
        {
            _loggingIn = true;
            StateHasChanged();
        }

        private void HideLoadingButton()
        {
            _loggingIn = false;
            StateHasChanged();
        }

        private void ShowErrorInvalidUser()
        {
            var invalidUser = "El usuario ingresado es incorrecto.";
            ShowErrorToUser(invalidUser);
        }

        private User GetUserFromForm()
        {
            var userName = _userField.Value;
            var password = _passwordField.Value;
            var user = new User { Name = userName, Password = password };
            return user;
        }

        private void RedirectToRoleHomePage(ClaimsPrincipal user)
        {

            if (user.IsInRole("IsCustomer"))
            {
                _navigationManager.NavigateTo("/customer/home");
            }

            if (user.IsInRole("IsEmployee"))
            {
                _navigationManager.NavigateTo("/employee/home");
            }

            ShowErrorCantRedirectUser();
        }

        private void ShowErrorCantRedirectUser()
        {
            var cantRedirectUser = "El usuario no tiene un rol asignado por lo que no puede ser redireccionado a la página de inicio" +
                ", por favor contáctese con soporte técnico.";
            ShowErrorToUser(cantRedirectUser);
        }

        private void ShowErrorToUser(string error)
        {
            Errors.Add(error);
            StateHasChanged();
        }

        private async Task<ClaimsPrincipal> GetUserAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user;
        }
    }
}
