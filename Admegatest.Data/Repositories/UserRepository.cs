using Admegatest.Core.Models;
using Admegatest.Core.Models.AuthenticationAndAuthorization;
using Admegatest.Data.InterfacesForRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AdmegatestDBContext _admegatestDBContext;
        private readonly JWTSettings _jwtsettings;

        public UserRepository(AdmegatestDBContext admegatestDBContext, IOptions<JWTSettings> jwtsettings)
        {
            _admegatestDBContext = admegatestDBContext;
            _jwtsettings = jwtsettings.Value;
        }

        public async Task<UserWithToken?> Login(User user)
        {
            var userFound = await _admegatestDBContext.Users.Include(u => u.Role)
                .Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();

            if (userFound == null)
            {
                return null;
            }

            var refreshToken = GenerateRefreshToken();
            userFound.RefreshTokens.Add(refreshToken);
            await _admegatestDBContext.SaveChangesAsync();

            var userWithToken = new UserWithToken(userFound);
            userWithToken.RefreshToken = refreshToken.Token;
            userWithToken.AccessToken = GenerateAccessToken(userFound.UserId);

            return userWithToken;
        }

        private string GenerateAccessToken(int userId)
        {
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(1);

            return refreshToken;
        }

        public async Task<User> GetUserByAccessToken(string accessToken)
        {
            User user = await GetUserFromAccessToken(accessToken);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        private async Task<User> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                    return await _admegatestDBContext.Users.Include(u => u.Role)
                                        .Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {
                return new User();
            }

            return new User();
        }
    }
}
