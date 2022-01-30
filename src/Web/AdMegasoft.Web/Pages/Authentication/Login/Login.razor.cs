﻿using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Application.Models;
using AdMegasoft.Application.Validators;
using AdMegasoft.Web.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

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

        private UnauthorizedUserModel _model = new UnauthorizedUserModel();
        private UnauthorizedUserModelValidator _validator = new UnauthorizedUserModelValidator();
        private MudForm _form;

        private bool _userEnteredIsIncorrect;

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        private async Task LoginAsync()
        {
            var userModel = await _userService.LoginAsync(_model);

            if (userModel == null)
            {
                _userEnteredIsIncorrect = true;
            }
            else
            {
                var adMegasoftAuthenticationStateProvider = (AdMegasoftAuthenticationStateProvider)_authenticationStateProvider;
                await adMegasoftAuthenticationStateProvider.MarkUserAsAuthenticated(userModel);
                _navigationManager.NavigateTo("/dashboard");
            }
        }

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

        private void IsValidChanged()
        {
            StateHasChanged();
        }
    }
}
