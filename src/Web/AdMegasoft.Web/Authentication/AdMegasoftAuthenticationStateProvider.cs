﻿using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Application.Models;
using AdMegasoft.Shared.Constants.Storage;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AdMegasoft.Web.Authentication
{
    public class AdMegasoftAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }
        public IUserService _userService { get; set; }

        public AdMegasoftAuthenticationStateProvider(ILocalStorageService localStorageService,
            IUserService userService)
        {
            _localStorageService = localStorageService;
            _userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>(StorageConstants.LocalStorage.AccessToken);

            ClaimsIdentity identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(accessToken))
            {
                var userModel = await _userService.GetUserFromAccessTokenAsync(accessToken);

                if (userModel != null) // TODO: No deberia retornar NULL, evitar referencias nulas. Fijarse si el token es valido primero y despues obtenerlo
                {
                    identity = GetClaimsIdentity(userModel);
                }
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticatedAsync(UserModel userModel)
        {
            await _localStorageService.SetItemAsync(StorageConstants.LocalStorage.AccessToken, userModel.AccessToken);

            var identity = GetClaimsIdentity(userModel);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await _localStorageService.RemoveItemAsync(StorageConstants.LocalStorage.AccessToken);

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(UserModel userModel)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userModel.Name),
            }, "jwt");

            var claimsRoles = userModel.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name));
            claimsIdentity.AddClaims(claimsRoles);

            return claimsIdentity;
        }

    }
}