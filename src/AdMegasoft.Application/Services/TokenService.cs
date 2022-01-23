using AdMegasoft.Application.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdMegasoft.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IJSRuntime _jsRuntime;

        public TokenService(IJSRuntime jsRuntime, IOptions<JWTSettings> jwtsettings)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("TokenManager.getToken");
        }

        public async Task<bool> SaveTokenAsync(string token)
        {
            return await _jsRuntime.InvokeAsync<bool>("TokenManager.saveToken", token);
        }

        public async Task<bool> DestroyTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<bool>("TokenManager.destroyToken");
        }
    }
}
