using Admegatest.Core.Models;
using Admegatest.Services.Authentication;
using Admegatest.Services.IServices;
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

        private bool success;
        private MudTextField<string> _emailField;
        private MudTextField<string> _passwordField;
        private List<string> Errors = new List<string>();

        protected async override Task OnInitializedAsync()
        {
            await CheckIfUserIsAuthenticated();
        }

        private async Task CheckIfUserIsAuthenticated()
        {
            var user = await GetUserAsync();

            if (user.Identity.IsAuthenticated)
            {
                await RedirectToRoleHomePageAsync();
            }
        }

        private async void ValidateUser()
        {
            Errors.Clear();
            var user = GetUserFromForm();
            var returnedUser = await _userService.Login(user);

            if (returnedUser == null)
            {
                ShowErrorInvalidUser();
            }
            else
            {
                var admegatestAuthenticationStateProvider = (AdmegatestAuthenticationStateProvider)_authenticationStateProvider;
                await admegatestAuthenticationStateProvider.MarkUserAsAuthenticated(returnedUser);
                await RedirectToRoleHomePageAsync();
            }

        }

        private void ShowErrorInvalidUser()
        {
            var invalidUser = "El usuario ingresado es incorrecto.";
            ShowErrorToUser(invalidUser);
        }

        private User GetUserFromForm()
        {
            var email = _emailField.Value;
            var password = _passwordField.Value;
            var user = new User { Email = email, Password = password };
            return user;
        }

        private async Task RedirectToRoleHomePageAsync()
        {
            var user = await GetUserAsync();

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
