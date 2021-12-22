using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Admegatest.Core.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Admegatest.Services.Authentication
{
    public class AdmegatestAuthenticationStateProvider : AuthenticationStateProvider
    {
        public AdmegatestAuthenticationStateProvider()
        {

        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task MarkUserAsAuthenticated(User user)
        {
            throw new NotImplementedException();
        }

        public async Task MarkUserAsLoggedOut()
        {
            throw new NotImplementedException();
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.Email != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.RoleDescription),
                }, "apiauth_type");
            }

            return claimsIdentity;
        }
    }
}
