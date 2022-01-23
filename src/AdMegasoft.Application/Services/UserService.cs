using AdMegasoft.Application.Configurations;
using AdMegasoft.Core.Abstractions;
using AdMegasoft.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdMegasoft.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTSettings _jwtsettings;

        public const int TheTokenHasNoId = -1;

        public UserService(IUserRepository userRepository, IOptions<JWTSettings> jwtsettings)
        {
            _userRepository = userRepository;
            _jwtsettings = jwtsettings.Value;
        }

        public void Login(string name, string password)
        {
            var userFound = _userRepository.GetUserByPasswordNameAsync(name, password);

            if (userFound == null)
            {
                // return LoginAttempResponse
            }

            //userFound.Token = GenerateAccessToken(userFound.Id);
            //return userFound;
        }

        public async Task<User> GetUserFromTokenAsync(string token)
        {
            var userId = GetUserIdFromToken(token);

            if(userId == TheTokenHasNoId)
            {
                //return null; return UserFromTokenResponse
            }

            return await _userRepository.GetByIdAsync(userId);
        }

        #region Private methods
        private string GenerateAccessToken(int userId)
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

            var oneDay = DateTime.UtcNow.AddDays(1);

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
        private int GetUserIdFromToken(string token)
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
        #endregion

    }
}
