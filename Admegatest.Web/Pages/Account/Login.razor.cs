using Admegatest.Core.Models;
using Admegatest.Services.Authentication;
using Admegatest.Services.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

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

        protected async override Task OnInitializedAsync()
        {
            await CheckIfUserIsAuthenticated();
        }

        private async Task CheckIfUserIsAuthenticated()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var claimsPrincipal = authState.User;

            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                //RedirectToRoleHomePage(); 
            }
        }

        private async void ValidateUser()
        {
            var user = GetUserFromForm();
            var returnedUser = await _userService.Login(user);

            if (returnedUser == null)
            {

            }
            else
            {
                var admegatestAuthenticationStateProvider = (AdmegatestAuthenticationStateProvider)_authenticationStateProvider;
                await admegatestAuthenticationStateProvider.MarkUserAsAuthenticated(returnedUser);
                RedirectToRoleHomePage();
            }

        }

        private User GetUserFromForm()
        {
            var email = _emailField.Value;
            var password = _passwordField.Value;
            var user = new User { Email = email, Password = password };
            return user;
        }

        private void RedirectToRoleHomePage()
        {
            _navigationManager.NavigateTo("/index");
        }
    }
}
