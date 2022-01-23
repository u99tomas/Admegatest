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
        private readonly JWTSettings _jwtsettings;

        public const int TheTokenHasNoId = -1;

        public TokenService(IJSRuntime jsRuntime, IOptions<JWTSettings> jwtsettings)
        {
            _jsRuntime = jsRuntime;
            _jwtsettings = jwtsettings.Value;
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

        public int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtsettings.Key);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken securityToken;
            ClaimsPrincipal claimsPrincipal;

            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            }
            catch (Exception)
            {
                return TheTokenHasNoId;
            }

            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null)
            {
                return TheTokenHasNoId;
            }

            bool signatureAlgorithmIsValid = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);

            if (signatureAlgorithmIsValid)
            {
                var userId = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
                return userId == null ? TheTokenHasNoId : Convert.ToInt32(userId);
            }

            return TheTokenHasNoId;
        }
    }
}
