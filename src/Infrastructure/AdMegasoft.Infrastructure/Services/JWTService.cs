using AdMegasoft.Application.Configurations;
using AdMegasoft.Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdMegasoft.Infrastructure.Services
{
    public class JWTService : IJWTService
    {
        private readonly JWTSettings _jwtsettings;

        public JWTService(IOptions<JWTSettings> jwtsettings)
        {
            _jwtsettings = jwtsettings.Value;
        }

        public string GenerateAccessToken(int userId)
        {
            var tokenDescriptor = GetSecurityTokenDescriptor(userId);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(int userId)
        {
            var key = Encoding.ASCII.GetBytes(_jwtsettings.Key);

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, Convert.ToString(userId))
            });

            var oneDay = DateTime.UtcNow.AddDays(_jwtsettings.TokenDurationInDays);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = oneDay,
                SigningCredentials = signingCredentials,
            };

            return tokenDescriptor;
        }

        public int? GetUserIdFromToken(string token)
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
                return null;
            }

            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null)
            {
                return null;
            }

            bool signatureAlgorithmIsValid = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);

            if (signatureAlgorithmIsValid)
            {
                var userId = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
                return userId == null ? null : Convert.ToInt32(userId);
            }

            return null;
        }
    }
}
